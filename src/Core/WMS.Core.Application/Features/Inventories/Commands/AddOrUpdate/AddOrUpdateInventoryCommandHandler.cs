using WMS.Core.Application.Abstractions.Messaging;
using WMS.Core.Application.Contracts.Responses.Inventories;
using WMS.Core.Domain.Entities;
using WMS.Core.Domain.Enums;
using WMS.Core.Domain.Shared.QueryParams;
using WMS.Core.Domain.Shared.Results;
using WMS.Core.Infrastructure.Data.Repositories.Core;
using WMS.Core.Infrastructure.Data.Uow;

namespace WMS.Core.Application.Features.Inventories.Commands.AddOrUpdate;

internal sealed class AddOrUpdateInventoryCommandHandler(IUnitOfWork unitOfWork)
    : ICommandHandler<AddOrUpdateInventoryCommand, int>
{
    private IGenericRepository<Inventory> InventoryRepository => unitOfWork.GetRepository<Inventory>();

    public async Task<Result<int>> Handle(AddOrUpdateInventoryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await unitOfWork.BeginTransaction();
            var queryOptions = new QueryOptions<Inventory, InventoryResponse>
            {
                Selector = inv => new InventoryResponse
                {
                    LocationId = inv.LocationId,
                    Quantity = inv.Quantity,
                    RowId = inv.RowId
                },
                Predicate = inv => inv.ProductId == request.InventoryRequest.ProductId
                                   && inv.LocationId == request.InventoryRequest.LocationId
                                   && inv.IsActive,
                CancellationToken = cancellationToken
            };

            var localItem = await InventoryRepository.GetSingleAsync(queryOptions);

            var inventory = Inventory.Create(
                request.InventoryRequest.ProductId,
                request.InventoryRequest.LocationId,
                request.InventoryRequest.Quantity);

            if (localItem is null)
            {
                InventoryRepository.Insert(inventory);
                await unitOfWork.SaveChangesAsync(cancellationToken);
            }
            else
            {
                switch (request.InventoryRequest.TransactionType)
                {
                    case InventoryTransactionType.In:
                        inventory.Quantity += localItem.Quantity;
                        inventory.RowId = localItem.RowId;

                        InventoryRepository.Update(inventory);
                        await unitOfWork.SaveChangesAsync(cancellationToken);
                        break;
                    case InventoryTransactionType.Out:
                        inventory.Quantity = localItem.Quantity - inventory.Quantity;
                        inventory.RowId = localItem.RowId;

                        if (inventory.Quantity == 0)
                        {
                            inventory.IsActive = false;
                        }
                        InventoryRepository.Update(inventory);
                        await unitOfWork.SaveChangesAsync(cancellationToken);
                        break;
                    case InventoryTransactionType.Transfer:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            await unitOfWork.CommitTransaction();

            return inventory.RowId;
        }
        catch
        {
            await unitOfWork.RollbackTransaction();
            throw;
        }
    }
}
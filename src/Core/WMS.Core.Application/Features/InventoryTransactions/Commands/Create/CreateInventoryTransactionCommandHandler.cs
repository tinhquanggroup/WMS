using WMS.Core.Application.Abstractions.Messaging;
using WMS.Core.Domain.Entities;
using WMS.Core.Domain.Shared.Results;
using WMS.Core.Infrastructure.Data.Uow;

namespace WMS.Core.Application.Features.InventoryTransactions.Commands.Create;

internal sealed class CreateInventoryTransactionCommandHandler(IUnitOfWork unitOfWork)
    : ICommandHandler<CreateInventoryTransactionCommand, int>
{
    public async Task<Result<int>> Handle(
        CreateInventoryTransactionCommand request,
        CancellationToken cancellationToken)
    {
        var internalRepository = unitOfWork.GetRepository<InventoryTransaction>();
        await unitOfWork.BeginTransaction();

        var inventoryTransaction = InventoryTransaction.Create(
            request.InventoryTransaction.InventoryId,
            request.InventoryTransaction.Quantity,
            request.InventoryTransaction.Type,
            request.InventoryTransaction.Status,
            request.InventoryTransaction.Reason ?? "",
            request.InventoryTransaction.IsCompleted
        );
        try
        {
            internalRepository.Insert(inventoryTransaction);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            await unitOfWork.CommitTransaction();
        }
        catch
        {
            await unitOfWork.RollbackTransaction();
        }
        return inventoryTransaction.RowId;
    }
}
using WMS.Core.Application.Abstractions.Messaging;
using WMS.Core.Application.Contracts.Responses.Inventories;
using WMS.Core.Application.Contracts.Responses.Locations;
using WMS.Core.Domain.Entities;
using WMS.Core.Domain.Enums;
using WMS.Core.Domain.Shared;
using WMS.Core.Domain.Shared.QueryParams;
using WMS.Core.Domain.Shared.Results;
using WMS.Core.Infrastructure.Data.Repositories.Core;
using WMS.Core.Infrastructure.Data.Uow;

namespace WMS.Core.Application.Features.Locations.Queries.GetAvailable;

internal sealed class GetLocationsAvailableQueryHandler(IUnitOfWork unitOfWork)
    : IQueryHandler<GetLocationsAvailableQuery, List<LocationResponse>>
{
    private IGenericRepository<Location> LocationRepository => unitOfWork.GetRepository<Location>();
    private IGenericRepository<Inventory> InventoryRepository => unitOfWork.GetRepository<Inventory>();

    public async Task<Result<List<LocationResponse>>> Handle(
        GetLocationsAvailableQuery request,
        CancellationToken cancellationToken)
    {
        var locationQueryOptions = new QueryOptions<Location, LocationResponse>
        {
            Selector = loc => new LocationResponse
            {
                LocationId = loc.RowId,
                Code = loc.Code,
                Name = loc.Name,
                PointX = loc.PointX,
                PointY = loc.PointY,
                PointZ = loc.PointZ
            },
            Predicate = loc => loc.IsActive && !loc.Inventories.Any(),
            CancellationToken = cancellationToken
        };

        var availableLocations = await LocationRepository.GetMultipleAsync(locationQueryOptions);

        var inventoryQueryOptions = new QueryOptions<Inventory, InventoryResponse>
        {
            Selector = inv => new InventoryResponse
            {
                RowId = inv.RowId,
                Location = inv.Location,
                Quantity = inv.Quantity
            },
            Predicate = inv => inv.ProductId == request.LocationAvailableRequest.ProductId && inv.IsActive,
            CancellationToken = cancellationToken
        };
        var inventories = await InventoryRepository.GetMultipleAsync(inventoryQueryOptions);

        if (!inventories.Any()) return availableLocations;

        switch (request.LocationAvailableRequest.TransactionType)
        {
            case InventoryTransactionType.In:
            {
                var result = inventories
                    .Where(i => (i.Location.Capacity - i.Quantity) >= request.LocationAvailableRequest.Quantity)
                    .OrderBy(i => i.Location.PointY)
                    .Select(i => new LocationResponse
                    {
                        LocationId = i.Location.RowId,
                        Code = i.Location.Code,
                        Name = i.Location.Name,
                        PointZ = i.Location.PointZ,
                        PointY = i.Location.PointY,
                        PointX = i.Location.PointX,
                    });

                return result.Concat(availableLocations).ToList();
            }
            case InventoryTransactionType.Out:
            {
                var result = inventories
                    .Where(i => i.Quantity >= 0)
                    .OrderBy(i => i.Location.PointY);
                return result.Select(i => new LocationResponse
                {
                    LocationId = i.Location.RowId,
                    Code = i.Location.Code,
                    Name = i.Location.Name,
                    PointZ = i.Location.PointZ,
                    PointY = i.Location.PointY,
                    PointX = i.Location.PointX,
                }).ToList();
            }
            case InventoryTransactionType.Transfer:
            default:
                return availableLocations;
        }
    }
}
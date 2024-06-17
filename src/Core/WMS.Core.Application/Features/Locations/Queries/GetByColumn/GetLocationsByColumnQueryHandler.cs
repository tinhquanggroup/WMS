using System.Linq.Expressions;
using WMS.Core.Application.Abstractions.Messaging;
using WMS.Core.Application.Contracts.Responses.Locations;
using WMS.Core.Domain.Entities;
using WMS.Core.Domain.Shared;
using WMS.Core.Domain.Shared.QueryParams;
using WMS.Core.Domain.Shared.Results;
using WMS.Core.Infrastructure.Data.Uow;

namespace WMS.Core.Application.Features.Locations.Queries.GetByColumn;

internal sealed class GetLocationsByColumnQueryHandler(IUnitOfWork unitOfWork)
    : IQueryHandler<GetLocationsByColumnQuery, List<LocationResponse>>
{
    public async Task<Result<List<LocationResponse>>> Handle(
        GetLocationsByColumnQuery request,
        CancellationToken cancellationToken)
    {
        var locationRepository = unitOfWork.GetRepository<Location>();

        Expression<Func<Location, bool>> filter = request.ColumnName.ToLower() switch
        {
            "location_code" => location => location.Code.Equals(request.Value),
            "zone_id" => location => location.ZoneId.Equals(Convert.ToInt32(request.Value)),
            _ => throw new ArgumentException("Invalid column name for filtering."),
        };

        var queryOptions = new QueryOptions<Location, LocationResponse>
        {
            Selector = loc => new LocationResponse
            {
                Code = loc.Code,
                Name = loc.Name,
                PointX = loc.PointX,
                PointY = loc.PointY,
                PointZ = loc.PointZ,
                Zone = loc.Zone
            },
            Predicate = filter,
            CancellationToken = cancellationToken
        };

        return await locationRepository.GetMultipleAsync(queryOptions);
    }
}
using WMS.Core.Application.Abstractions.Messaging;
using WMS.Core.Application.Contracts.Responses.Common;
using WMS.Core.Domain.Entities;
using WMS.Core.Domain.Shared.QueryParams;
using WMS.Core.Domain.Shared.Results;
using WMS.Core.Infrastructure.Data.Repositories.Core;
using WMS.Core.Infrastructure.Data.Uow;

namespace WMS.Core.Application.Features.Zones.Queries.GetOption;

internal sealed class GetZonesOptionQueryHandler(IUnitOfWork unitOfWork)
    : IQueryHandler<GetZonesOptionQuery, List<ComboboxItemResponse>>
{
    private readonly IGenericRepository<Zone> _zoneRepository = unitOfWork.GetRepository<Zone>();
    public async Task<Result<List<ComboboxItemResponse>>> Handle(GetZonesOptionQuery request, CancellationToken cancellationToken)
    {
        var queryOptions = new QueryOptions<Zone, ComboboxItemResponse>
        {
            Selector = z => new ComboboxItemResponse
            {
                Key = z.RowId,
                Val = z.Name
            },
            CancellationToken = cancellationToken
        };

        return await _zoneRepository.GetMultipleAsync(queryOptions);
    }
}
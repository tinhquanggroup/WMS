using WMS.Core.Application.Abstractions.Messaging;
using WMS.Core.Application.Contracts.Requests.Locations;
using WMS.Core.Application.Contracts.Responses.Locations;

namespace WMS.Core.Application.Features.Locations.Queries.GetAvailable;

public record GetLocationsAvailableQuery(LocationAvailableRequest LocationAvailableRequest) : IQuery<List<LocationResponse>>;
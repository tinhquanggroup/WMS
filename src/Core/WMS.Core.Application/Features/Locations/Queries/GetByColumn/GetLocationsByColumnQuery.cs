using WMS.Core.Application.Abstractions.Messaging;
using WMS.Core.Application.Contracts.Responses.Locations;

namespace WMS.Core.Application.Features.Locations.Queries.GetByColumn;

public record GetLocationsByColumnQuery(string ColumnName, string Value) : IQuery<List<LocationResponse>>;
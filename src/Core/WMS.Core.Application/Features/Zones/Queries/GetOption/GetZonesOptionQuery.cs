using WMS.Core.Application.Abstractions.Messaging;
using WMS.Core.Application.Contracts.Responses.Common;

namespace WMS.Core.Application.Features.Zones.Queries.GetOption;

public record GetZonesOptionQuery : IQuery<List<ComboboxItemResponse>>;
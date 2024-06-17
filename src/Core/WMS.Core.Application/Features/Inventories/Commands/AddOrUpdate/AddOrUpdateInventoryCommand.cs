using WMS.Core.Application.Abstractions.Messaging;
using WMS.Core.Application.Contracts.Requests.Inventories;

namespace WMS.Core.Application.Features.Inventories.Commands.AddOrUpdate;

public record AddOrUpdateInventoryCommand(AddOrUpdateInventoryRequest InventoryRequest) : ICommand<int>;
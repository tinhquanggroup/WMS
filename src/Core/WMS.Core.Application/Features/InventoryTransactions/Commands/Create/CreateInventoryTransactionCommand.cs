using WMS.Core.Application.Abstractions.Messaging;
using WMS.Core.Application.Contracts.Requests.InventoryTransactions;

namespace WMS.Core.Application.Features.InventoryTransactions.Commands.Create;

public record CreateInventoryTransactionCommand(CreateInventoryTransactionRequest InventoryTransaction) : ICommand<int>;
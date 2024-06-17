using WMS.Core.Domain.Enums;

namespace WMS.Core.Application.Contracts.Requests.InventoryTransactions;

public record CreateInventoryTransactionRequest
(
    int InventoryId,
    int Quantity,
    InventoryTransactionType Type,
    InventoryTransactionStatus Status,
    string? Reason,
    bool IsCompleted
);
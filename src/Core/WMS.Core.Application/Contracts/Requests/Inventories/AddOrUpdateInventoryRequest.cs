using WMS.Core.Domain.Enums;

namespace WMS.Core.Application.Contracts.Requests.Inventories;

public record AddOrUpdateInventoryRequest
(
     int ProductId,
     int LocationId,
     int Quantity,
     InventoryTransactionType TransactionType
);
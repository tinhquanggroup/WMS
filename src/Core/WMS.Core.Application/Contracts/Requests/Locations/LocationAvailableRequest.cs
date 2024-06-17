using WMS.Core.Domain.Enums;

namespace WMS.Core.Application.Contracts.Requests.Locations;

public record LocationAvailableRequest(
    int ProductId,
    int Quantity,
    InventoryTransactionType TransactionType);
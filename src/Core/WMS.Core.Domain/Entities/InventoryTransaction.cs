using System.ComponentModel.DataAnnotations.Schema;
using WMS.Core.Domain.Abstractions;
using WMS.Core.Domain.Enums;
using WMS.Core.Domain.Utils;

namespace WMS.Core.Domain.Entities;


[Table(name: "InventoryTransactions")]
public class InventoryTransaction : BaseEntity
{
    public DateTime TransactionDate { get; private set; }
    public int Quantity { get; private set; }
    public InventoryTransactionStatus TransactionStatus { get; private set; }
    public InventoryTransactionType TransactionType { get; private set; }
    public string Reason { get; private set; } = string.Empty;
    public bool IsCompleted { get; private set; }

    public int InventoryId { get; set; }

    [ForeignKey(nameof(InventoryId))]
    public virtual Inventory Inventory { get; set; } = null!;

    private InventoryTransaction() { }

    public static InventoryTransaction Create(
        int inventoryId,
        int quantity,
        InventoryTransactionType type,
        InventoryTransactionStatus status,
        string reason,
        bool isCompleted
    )
    {
        var result = new InventoryTransaction
        {
            InventoryId = inventoryId,
            Quantity = quantity,
            TransactionType = type,
            TransactionStatus = status,
            Reason = reason,
            IsCompleted = isCompleted,
            IsActive = true,
            IsDeleted = false,
            CreatedAt = TimeHelper.GetCurrentTime(),
            UpdatedAt = TimeHelper.GetCurrentTime()
        };

        return result;
    }
}
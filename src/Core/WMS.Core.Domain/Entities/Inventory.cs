using System.ComponentModel.DataAnnotations.Schema;
using WMS.Core.Domain.Abstractions;
using WMS.Core.Domain.Utils;

namespace WMS.Core.Domain.Entities;

[Table("Inventories")]
public class Inventory : BaseEntity
{
    public int ProductId { get; private set; }
    public int LocationId { get; private set; }
    public int Quantity { get; set; }

    [ForeignKey(nameof(LocationId))]
    public virtual Location Location { get; set; } = null!;

    [ForeignKey(nameof(ProductId))]
    public virtual Product Product { get; set; } = null!;

    [InverseProperty(nameof(Inventory))]
    public virtual ICollection<InventoryTransaction> InventoryTransactions { get; set; } = [];

    private Inventory() { }

    public static Inventory Create(int productId, int locationId, int quantity)
    {
        var inventory = new Inventory
        {
            ProductId = productId,
            LocationId = locationId,
            Quantity = quantity,
            IsActive = true,
            CreatedAt = TimeHelper.GetCurrentTime(),
            UpdatedAt = TimeHelper.GetCurrentTime(),
            IsDeleted = false
        };

        return inventory;
    }

}
using WMS.Core.Domain.Entities;

namespace WMS.Core.Application.Contracts.Responses.Inventories;

public record InventoryResponse
{
    public int RowId { get; set; }
    public int ProductId { get; set; }
    public int LocationId { get; set; }
    public int Quantity { get; set; }
    public Location Location { get; set; } = null!;
    public Product Product { get; set; } = null!;
    public ICollection<InventoryTransaction> InventoryTransactions { get; set; } = [];
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsActive { get; set; }
};
namespace WMS.Core.Domain.Abstractions;

public abstract class BaseEntity
{
    public int RowId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsActive { get; set; }
}
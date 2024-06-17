using System.ComponentModel.DataAnnotations.Schema;

namespace WMS.Core.Domain.Entities;

[Table("ProductAttributes")]
public class ProductAttribute
{
    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;

    public int AttributeId { get; set; }
    public Attribute Attribute { get; set; } = null!;
}
using System.ComponentModel.DataAnnotations.Schema;
using WMS.Core.Domain.Abstractions;

namespace WMS.Core.Domain.Entities;

[Table("ProductCategories")]
public class ProductCategory : BaseEntity
{
    public string Name { get; private set; } = null!;

    [InverseProperty(nameof(ProductCategory))]
    public virtual ICollection<Product> Products { get; set; } = [];

    private ProductCategory() { }
}
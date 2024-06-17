using System.ComponentModel.DataAnnotations.Schema;
using WMS.Core.Domain.Abstractions;

namespace WMS.Core.Domain.Entities;

[Table("Attributes")]
public class Attribute : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Value { get; set; } = null!;

    [InverseProperty(nameof(Attribute))]
    public virtual ICollection<ProductAttribute> ProductAttributes { get; set; } = [];

    private Attribute() { }
}
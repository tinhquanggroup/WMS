using System.ComponentModel.DataAnnotations.Schema;
using WMS.Core.Domain.Abstractions;
using WMS.Core.Domain.Enums;
using WMS.Core.Domain.Utils;

namespace WMS.Core.Domain.Entities;

[Table(name: "Products")]
public class Product : BaseEntity
{
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string ImagePath { get; set; } = string.Empty;
    public ExpirationType ExpirationPeriodType { get; private set; }
    public int ExpirationPeriodValue { get; private set; }
    public int ProductCategoryId { get; set; }

    [ForeignKey(nameof(ProductCategoryId))]
    public virtual ProductCategory ProductCategory { get; set; } = null!;

    [InverseProperty(nameof(Product))]
    public virtual ICollection<ProductAttribute> ProductAttributes { get; set; } = [];

    private Product() { }

    public static Product Create(
        string code,
        string name,
        string imagePath,
        int expirationPeriodType,
        int expirationPeriodValue,
        int productCategoryId)
    {
        var product = new Product
        {
            Code = code,
            Name = name,
            ImagePath = imagePath,
            ExpirationPeriodType = (ExpirationType)expirationPeriodType,
            ExpirationPeriodValue = expirationPeriodValue,
            ProductCategoryId = productCategoryId,
            IsActive = true,
            CreatedAt = TimeHelper.GetCurrentTime(),
            UpdatedAt = TimeHelper.GetCurrentTime(),
            IsDeleted = false
        };

        return product;
    }
}
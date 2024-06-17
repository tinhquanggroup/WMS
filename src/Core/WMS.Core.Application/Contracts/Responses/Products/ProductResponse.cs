using WMS.Core.Domain.Enums;

namespace WMS.Core.Application.Contracts.Responses.Products;

public record ProductResponse
{
    public int ProductId { get; set; }
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string ImagePath { get; set; } = null!;
    public ExpirationType ExpirationPeriodType { get; set; }
    public int ExpirationPeriodValue { get; set; }
    public int ProductCategoryId { get; set; }
}
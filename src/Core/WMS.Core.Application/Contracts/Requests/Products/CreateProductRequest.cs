namespace WMS.Core.Application.Contracts.Requests.Products;

public record CreateProductRequest(
    string Code,
    string Name,
    int ExpirationPeriodType,
    int ExpirationPeriodValue,
    string ImagePath,
    int CategoryId);
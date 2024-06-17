using WMS.WF.Application.Enums;

namespace WMS.WF.Application.Contracts.Responses.Products;

public record ProductResponse(
    string Code,
    string Name, 
    string ImagePath, 
    ExpirationType ExpirationPeriodType, 
    int ExpirationPeriodValue,
    int ProductCategoryId);
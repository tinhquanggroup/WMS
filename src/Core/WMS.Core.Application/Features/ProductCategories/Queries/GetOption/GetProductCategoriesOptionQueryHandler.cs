using WMS.Core.Application.Abstractions.Messaging;
using WMS.Core.Application.Contracts.Responses.Common;
using WMS.Core.Domain.Entities;
using WMS.Core.Domain.Shared.QueryParams;
using WMS.Core.Domain.Shared.Results;
using WMS.Core.Infrastructure.Data.Uow;

namespace WMS.Core.Application.Features.ProductCategories.Queries.GetOption;

internal sealed class GetProductCategoriesOptionQueryHandler(IUnitOfWork unitOfWork)
    : IQueryHandler<GetProductCategoriesOptionQuery, List<ComboboxItemResponse>>
{
    public async Task<Result<List<ComboboxItemResponse>>> Handle(
        GetProductCategoriesOptionQuery request,
        CancellationToken cancellationToken)
    {
        var productCategoryRepository = unitOfWork.GetRepository<ProductCategory>();
        var queryOption = new QueryOptions<ProductCategory, ComboboxItemResponse>
        {
            Selector = p => new ComboboxItemResponse
            {
                Key = p.RowId,
                Val = p.Name
            },
            CancellationToken = cancellationToken
        };
        return await productCategoryRepository.GetMultipleAsync(queryOption);
    }
}
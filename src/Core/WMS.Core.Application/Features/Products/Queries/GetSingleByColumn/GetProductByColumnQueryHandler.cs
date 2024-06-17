using System.Linq.Expressions;
using WMS.Core.Application.Abstractions.Messaging;
using WMS.Core.Application.Contracts.Responses.Products;
using WMS.Core.Domain.Entities;
using WMS.Core.Domain.Shared.Errors;
using WMS.Core.Domain.Shared.QueryParams;
using WMS.Core.Domain.Shared.Results;
using WMS.Core.Infrastructure.Data.Uow;

namespace WMS.Core.Application.Features.Products.Queries.GetSingleByColumn;

internal sealed class GetProductByColumnQueryHandler(IUnitOfWork unitOfWork)
    : IQueryHandler<GetProductByColumnQuery, ProductResponse>
{
    public async Task<Result<ProductResponse>> Handle(
        GetProductByColumnQuery request,
        CancellationToken cancellationToken)
    {
        var productRepository = unitOfWork.GetRepository<Product>();

        var predicate = GetPredicateProperty(request);

        var queryOptions = new QueryOptions<Product, ProductResponse>
        {
            Selector = p => new ProductResponse
            {
                ProductId = p.RowId,
                Code = p.Code,
                Name = p.Name,
                ImagePath = p.ImagePath,
                ProductCategoryId = p.ProductCategoryId,
                ExpirationPeriodType = p.ExpirationPeriodType,
                ExpirationPeriodValue = p.ExpirationPeriodValue
            },
            Predicate = predicate,
            CancellationToken = cancellationToken
        };

        var result = await productRepository.GetSingleAsync(queryOptions);

        if (result is null)
        {
            return Result.Failure<ProductResponse>(new Error(
                ErrorCodes.Product.NotFound,
                ErrorMessages.Product.NotFound(request.ColumnName, request.Value)));
        }

        return await productRepository.GetSingleAsync(queryOptions);
    }

    private static Expression<Func<Product, bool>> GetPredicateProperty(GetProductByColumnQuery request) =>
        request.ColumnName.ToLower() switch
        {
            "id" => product => product.RowId.Equals(TryParseProductId(request.Value)),
            "code" => product => product.Code.Equals(request.Value),
            _ => product => product.RowId.Equals(TryParseProductId(request.Value))
        };

    private static int TryParseProductId(string value)
    {
        int.TryParse(value, out var result);

        return result;
    }
}
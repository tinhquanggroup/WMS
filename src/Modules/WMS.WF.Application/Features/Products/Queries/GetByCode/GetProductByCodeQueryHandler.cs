using MediatR;
using Microsoft.Extensions.Options;
using WMS.WF.Application.Abstractions.Messaging;
using WMS.WF.Application.Contracts.Responses.Products;
using WMS.WF.Infrastructure.Configurations;
using WMS.WF.Infrastructure.Services;

namespace WMS.WF.Application.Features.Products.Queries.GetByCode
{
    internal sealed class GetProductByCodeQueryHandler(IApiClient apiClient, IOptions<ApiSettings> apiSettings)
        : IQueryHandler<GetProductByCodeQuery, ProductResponse?>
    {

        public async Task<ProductResponse?> Handle(GetProductByCodeQuery request, CancellationToken cancellationToken)
        {
            var result = await apiClient.GetSingleAsync<ProductResponse>(GetUri(request.ProductCode));
            return result;
        }

        private string GetUri(string productCode)
        {
            return apiSettings.Value.Endpoints.Product.GetByCode.Replace("{code}", productCode);
        }
    }
}

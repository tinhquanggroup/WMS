using WMS.WF.Application.Abstractions.Messaging;
using WMS.WF.Application.Contracts.Responses.Products;

namespace WMS.WF.Application.Features.Products.Queries.GetByCode;

public record GetProductByCodeQuery(string ProductCode) : IQuery<ProductResponse?>;
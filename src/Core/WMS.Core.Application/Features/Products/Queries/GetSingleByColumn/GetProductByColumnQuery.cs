using WMS.Core.Application.Abstractions.Messaging;
using WMS.Core.Application.Contracts.Responses.Products;

namespace WMS.Core.Application.Features.Products.Queries.GetSingleByColumn;

public record GetProductByColumnQuery(string ColumnName, string Value) : IQuery<ProductResponse>;
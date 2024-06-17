using WMS.Core.Application.Abstractions.Messaging;
using WMS.Core.Application.Contracts.Responses.Common;

namespace WMS.Core.Application.Features.ProductCategories.Queries.GetOption;

public record GetProductCategoriesOptionQuery : IQuery<List<ComboboxItemResponse>>;
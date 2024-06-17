using MediatR;
using Microsoft.AspNetCore.Mvc;
using WMS.Core.Api.Abstractions;
using WMS.Core.Application.Features.ProductCategories.Queries.GetOption;

namespace WMS.Core.Api.Controllers;

[Route("api/products/categories")]
public class ProductCategoriesController(ISender sender) : ApiController(sender)
{
    [HttpGet("options")]
    public async Task<IActionResult> GetOptions(CancellationToken cancellationToken)
    {
        var query = new GetProductCategoriesOptionQuery();

        var response = await Sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : NoContent();
    }
}
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WMS.Core.Api.Abstractions;
using WMS.Core.Application.Contracts.Requests.Products;
using WMS.Core.Application.Features.Products.Commands.Create;
using WMS.Core.Application.Features.Products.Queries.GetSingleByColumn;

namespace WMS.Core.Api.Controllers
{
    [Route("api/products")]
    public class ProductsController(ISender sender) : ApiController(sender)
    {
        [HttpGet("column")]
        public async Task<IActionResult> GetProductByColumn(
            [FromQuery] string columnName,
            [FromQuery] string value,
            CancellationToken cancellationToken)
        {
            var query = new GetProductByColumnQuery(columnName, value);

            var response = await Sender.Send(query, cancellationToken);

            return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(
            [FromBody] CreateProductRequest request,
            CancellationToken cancellationToken)
        {
            var command = new CreateProductCommand(request);

            var response = await Sender.Send(command, cancellationToken);

            if (response.IsFailure)
            {
                return HandleFailure(response);
            }

            return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
        }
    }

}

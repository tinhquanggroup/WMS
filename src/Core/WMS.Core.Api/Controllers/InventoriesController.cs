using MediatR;
using Microsoft.AspNetCore.Mvc;
using WMS.Core.Api.Abstractions;
using WMS.Core.Application.Contracts.Requests.Inventories;
using WMS.Core.Application.Features.Inventories.Commands.AddOrUpdate;

namespace WMS.Core.Api.Controllers;

[Route("api/inventories")]
public class InventoriesController(ISender sender) : ApiController(sender)
{
    [HttpPost]
    public async Task<IActionResult> AddOrUpdate(
        [FromQuery] AddOrUpdateInventoryRequest request,
        CancellationToken cancellationToken)
    {
        var query = new AddOrUpdateInventoryCommand(request);

        var response = await Sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : NoContent();
    }
}
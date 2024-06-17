using MediatR;
using Microsoft.AspNetCore.Mvc;
using WMS.Core.Api.Abstractions;
using WMS.Core.Application.Features.Zones.Queries.GetOption;

namespace WMS.Core.Api.Controllers;

[Route("api/zones")]
public sealed class ZonesController(ISender sender) : ApiController(sender)
{
    [HttpGet("options")]
    public async Task<IActionResult> GetZoneForCombobox(CancellationToken cancellationToken)
    {
        var query = new GetZonesOptionQuery();

        var response = await Sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : NoContent();
    }
}
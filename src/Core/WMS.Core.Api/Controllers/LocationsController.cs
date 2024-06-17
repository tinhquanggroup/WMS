using MediatR;
using Microsoft.AspNetCore.Mvc;
using WMS.Core.Api.Abstractions;
using WMS.Core.Application.Contracts.Requests.Locations;
using WMS.Core.Application.Features.Locations.Queries.GetAvailable;
using WMS.Core.Application.Features.Locations.Queries.GetByColumn;

namespace WMS.Core.Api.Controllers;

[Route("api/locations")]
public class LocationsController(ISender sender) : ApiController(sender)
{
    [HttpGet("column")]
    public async Task<IActionResult> GetLocationsByColumn(
        [FromQuery] string columnName,
        [FromQuery] string value,
        CancellationToken cancellationToken)
    {
        var query = new GetLocationsByColumnQuery(columnName, value);

        var response = await Sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : NoContent();
    }

    [HttpGet("available")]
    public async Task<IActionResult> GetLocationsAvailable(
        [FromQuery] LocationAvailableRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetLocationsAvailableQuery(request);

        var response = await Sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : NoContent();
    }
}
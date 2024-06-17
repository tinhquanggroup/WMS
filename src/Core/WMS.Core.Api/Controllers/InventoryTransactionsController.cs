using MediatR;
using Microsoft.AspNetCore.Mvc;
using WMS.Core.Api.Abstractions;
using WMS.Core.Application.Contracts.Requests.InventoryTransactions;
using WMS.Core.Application.Features.InventoryTransactions.Commands.Create;

namespace WMS.Core.Api.Controllers;

[Route("api/inventory-transactions")]
public class InventoryTransactionsController(ISender sender) : ApiController(sender)
{
    [HttpPost]
    public async Task<IActionResult> CreateInventoryTransaction(
        [FromQuery] CreateInventoryTransactionRequest request,
        CancellationToken cancellationToken)
    {
        var query = new CreateInventoryTransactionCommand(request);

        var response = await Sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : NoContent();
    }
}
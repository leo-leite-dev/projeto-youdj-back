using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YouDj.Api.Authorization.Policies;
using YouDj.Api.Helpers;
using YouDj.Api.Mappers;
using YouDj.Api.Requests.Queue;
using YouDj.Application.Features.SongOrders.Accept;
using YouDj.Application.Features.SongOrders.Reject;

namespace YouDj.Api.Controllers.Queue;

[ApiController]
[Route("api/queue/orders")]
public sealed class SongOrdersController : ControllerBase
{
    private readonly ISender _mediator;

    public SongOrdersController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Authorize(Policy = GuestOnlyPolicy.Name)]
    public async Task<IActionResult> Create(
        [FromBody] CreateDjSongOrderRequest request,
        CancellationToken ct)
    {
        var command = request.ToCommand(HttpContext);
        var result = await _mediator.Send(command, ct);
        return result.ToActionResult(this);
    }

    [HttpPost("{orderId}/accept")]
    [Authorize(Policy = DjOnlyPolicy.Name)]
    public async Task<IActionResult> Accept(
        Guid orderId,
        CancellationToken ct)
    {
        var command = new AcceptDjSongOrderCommand
        {
            OrderId = orderId,
            DjId = HttpContext.GetDjId()
        };

        var result = await _mediator.Send(command, ct);
        return result.ToActionResult(this);
    }

    [HttpPost("{orderId}/reject")]
    [Authorize(Policy = DjOnlyPolicy.Name)]
    public async Task<IActionResult> Reject(
        Guid orderId,
        CancellationToken ct)
    {
        var command = new RejectDjSongOrderCommand
        {
            OrderId = orderId,
            DjId = HttpContext.GetDjId()
        };

        var result = await _mediator.Send(command, ct);
        return result.ToActionResult(this);
    }
}
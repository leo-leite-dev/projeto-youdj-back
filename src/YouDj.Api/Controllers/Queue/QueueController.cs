using MediatR;
using Microsoft.AspNetCore.Mvc;
using YouDj.Api.Helpers;
using YouDj.Api.Mappers;

namespace YouDj.Api.Controllers.Queue;

[ApiController]
[Route("api/queue")]
public sealed class QueueController : ControllerBase
{
    private readonly ISender _mediator;

    public QueueController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> AddMusic(
        [FromBody] AddMusicRequest request,
        CancellationToken ct)
    {
        var command = request.ToCommand();
        var result = await _mediator.Send(command, ct);

        return result.ToActionResult(this);
    }
}
using MediatR;
using Microsoft.AspNetCore.Mvc;
using YouDj.Api.Helpers;
using YouDj.Api.Mappers;
using YouDj.Api.Requests;
using Microsoft.AspNetCore.Authorization;
using YouDj.Api.Authorization.Policies;
using YouDj.Application.Abstractions.Auth;
using YouDj.Application.Features.Queue.GetQueue;
using YouDj.Api.Requests.Queue;

namespace YouDj.Api.Controllers.Queue;

[ApiController]
[Route("api/queue")]
[Authorize(Policy = DjOnlyPolicy.Name)]
public sealed class QueueController : ControllerBase
{
    private readonly ISender _mediator;
    private readonly ICurrentDj _currentDj;

    public QueueController(
        ISender mediator,
        ICurrentDj currentDj)
    {
        _mediator = mediator;
        _currentDj = currentDj;
    }

    [HttpPost]
    public async Task<IActionResult> AddMusic(
        [FromBody] AddMusicToQueueRequest request,
        CancellationToken ct)
    {
        var command = request.ToCommand(_currentDj.DjId);
        var result = await _mediator.Send(command, ct);

        return result.ToActionResult(this);
    }

    [HttpPut("reorder")]
    public async Task<IActionResult> Reorder(
    [FromBody] ReorderQueueRequest request,
    CancellationToken ct)
    {
        var command = request.ToCommand(_currentDj.DjId);
        var result = await _mediator.Send(command, ct);

        return result.ToActionResult(this);
    }


    [HttpGet]
    public async Task<IActionResult> GetQueue(CancellationToken ct)
    {
        var query = new GetQueueQuery(_currentDj.DjId);
        var result = await _mediator.Send(query, ct);

        return result.ToActionResult(this);
    }
}
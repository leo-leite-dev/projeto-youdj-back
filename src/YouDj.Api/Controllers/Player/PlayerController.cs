using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YouDj.Api.Authorization.Policies;
using YouDj.Api.Contracts.Player;
using YouDj.Api.Helpers;
using YouDj.Api.Mappers.Player;
using YouDj.Application.Abstractions.Auth;
using YouDj.Application.Features.Player;

[ApiController]
[Route("api/player")]
[Authorize(Policy = DjOnlyPolicy.Name)]
public sealed class PlayerController : ControllerBase
{
    private readonly ISender _mediator;
    private readonly ICurrentDj _currentDj;

    public PlayerController(
        ISender mediator,
        ICurrentDj currentDj)
    {
        _mediator = mediator;
        _currentDj = currentDj;
    }

    [HttpPost("play-now")]
    public async Task<IActionResult> PlayNow(
        [FromBody] PlayNowRequest request,
        CancellationToken ct)
    {
        var command = request.ToCommand(_currentDj.DjId);
        var result = await _mediator.Send(command, ct);

        return result.ToActionResult(this);
    }

    [HttpGet("current")]
    [AllowAnonymous]
    public async Task<IActionResult> GetCurrent(
        [FromQuery] Guid djId,
        CancellationToken ct)
    {
        var dto = await _mediator.Send(
            new GetCurrentPlayingQuery(djId),
            ct
        );

        return dto is null
            ? NoContent()
            : Ok(dto.ToResponse());
    }

    [HttpPost("play-from-queue")]
    public async Task<IActionResult> PlayFromQueue(
        [FromBody] PlayFromQueueRequest request,
        CancellationToken ct)
    {
        var command = request.ToCommand(_currentDj.DjId);
        var result = await _mediator.Send(command, ct);

        return result.ToActionResult(this);
    }

    [HttpPost("finish-playing")]
    public async Task<IActionResult> FinishPlaying(
    [FromBody] FinishPlayingRequest request,
    CancellationToken ct)
    {
        var command = request.ToCommand(_currentDj.DjId);
        var result = await _mediator.Send(command, ct);

        return result.ToActionResult(this);
    }
}
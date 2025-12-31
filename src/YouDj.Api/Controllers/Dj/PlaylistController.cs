using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YouDj.Api.Authorization.Policies;
using YouDj.Api.Contracts.Playlists;
using YouDj.Api.Helpers;
using YouDj.Api.Mappers.Dj.Playlists;
using YouDj.Application.Abstractions.Auth;
using YouDj.Application.Features.Dj.Extension.Generate;
using YouDj.Application.Features.Playlists.GenerateQrCode;

namespace YouDj.Api.Controllers.Dj;

[ApiController]
[Route("api/dj/playlist")]
[Authorize(Policy = DjOnlyPolicy.Name)]
public sealed class PlaylistController : ControllerBase
{
    private readonly ISender _mediator;
    private readonly ICurrentDj _currentDj;

    public PlaylistController(
        ISender mediator,
        ICurrentDj currentDj)
    {
        _mediator = mediator;
        _currentDj = currentDj;
    }

    [HttpPost("qrcode")]
    public async Task<IActionResult> GenerateQrCode(CancellationToken ct)
    {
        var result = await _mediator.Send(
            new GeneratePlaylistQrCodeCommand(), ct);

        return result.ToActionResult(this);
    }

    [HttpPost("dj-extension")]
    public async Task<IActionResult> GenerateDjExtensionQrCode(CancellationToken ct)
    {
        var result = await _mediator.Send(
            new GenerateDjExtensionQrCodeCommand(), ct);

        return result.ToActionResult(this);
    }

    [HttpPost("item")]
    public async Task<IActionResult> Add(
    [FromBody] AddPlaylistItemRequest request,
    CancellationToken ct)
    {
        var command = request.ToCommand(_currentDj.DjId);
        var result = await _mediator.Send(command, ct);

        return result.ToActionResult(this);
    }

    [HttpPost("folder")]
    public async Task<IActionResult> Create(
    [FromBody] CreatePlaylistFolderRequest request,
    CancellationToken ct)
    {
        var command = request.ToCommand(_currentDj.DjId);
        var result = await _mediator.Send(command, ct);

        return result.ToActionResult(this);
    }

    [HttpGet("folder")]
    public async Task<IActionResult> GetFolders(
    [FromQuery] GetPlaylistFoldersRequest request,
    CancellationToken ct)
    {
        var query = request.ToQuery(_currentDj.DjId);
        var result = await _mediator.Send(query, ct);

        return result.ToActionResult(this);
    }
}
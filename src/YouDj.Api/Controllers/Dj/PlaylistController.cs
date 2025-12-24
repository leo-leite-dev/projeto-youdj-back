using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YouDj.Api.Authorization.Policies;
using YouDj.Api.Helpers;
using YouDj.Application.Features.Playlists.GenerateQrCode;

namespace YouDj.Api.Controllers.Dj;

[ApiController]
[Route("api/dj/playlist")]
[Authorize(Policy = DjOnlyPolicy.Name)]
public sealed class PlaylistController : ControllerBase
{
    private readonly IMediator _mediator;

    public PlaylistController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("qrcode")]
    public async Task<IActionResult> GenerateQrCode(CancellationToken ct)
    {
        var result = await _mediator.Send(
            new GeneratePlaylistQrCodeCommand(), ct);

        return result.ToActionResult(this);
    }
}
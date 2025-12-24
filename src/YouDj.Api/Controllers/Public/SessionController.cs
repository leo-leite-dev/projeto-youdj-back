using Microsoft.AspNetCore.Mvc;
using YouDj.Application.Abstractions.Auth;

namespace YouDj.Api.Controllers.Public;

[ApiController]
[Route("api/public/playlist")]
public sealed class SessionController : ControllerBase
{
    private readonly IPlaylistSessionTokenService _sessionService;

    public SessionController(IPlaylistSessionTokenService sessionService)
    {
        _sessionService = sessionService;
    }

    [HttpPost("{playlistToken}/session")]
    public async Task<IActionResult> Start(
        [FromRoute] string playlistToken,
        [FromBody] SessionRequest request)
    {
        var result = await _sessionService
            .IssueAsync(request.DisplayName, playlistToken);

        return Ok(result);
    }
}
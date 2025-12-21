using Microsoft.AspNetCore.Mvc;
using YouDj.Application.Abstractions.Auth;

[ApiController]
[Route("api/public/session")]
public sealed class SessionController : ControllerBase
{
    private readonly IGuestSessionTokenService _sessionService;

    public SessionController(IGuestSessionTokenService sessionService)
    {
        _sessionService = sessionService;
    }

    [HttpPost("start")]
    public IActionResult Start(
        [FromQuery] Guid djId,
        [FromBody] SessionRequest request)
    {
        var result = _sessionService.Issue(request.DisplayName, djId);
        return Ok(result);
    }
}
using BaitaHora.Api.Web.Cookies;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using YouDj.Api.Contracts.Auth;
using YouDj.Api.Helpers;
using YouDj.Api.Mappers.Auth;
using YouDj.Application.Abstractions.Web;

namespace YouDj.Api.Controllers.Auth;

[ApiController]
[Route("api/auth")]
public sealed class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IJwtCookieFactory _cookieFactory;
    private readonly IJwtCookieWriter _cookieWriter;

    public AuthController(
        IMediator mediator,
        IJwtCookieFactory cookieFactory,
        IJwtCookieWriter cookieWriter)
    {
        _mediator = mediator;
        _cookieFactory = cookieFactory;
        _cookieWriter = cookieWriter;
    }

    [HttpPost("login/dj")]
    public async Task<IActionResult> Login(
        [FromBody] LoginDjRequest request,
        CancellationToken ct)
    {
        var result = await _mediator.Send(request.ToCommand(), ct);

        if (!result.IsSuccess)
            return result.ToActionResult(this);

        var login = result.Value!;

        var cookie = _cookieFactory.CreateLoginCookie(
            login.AccessToken,
            login.ExpiresAtUtc - DateTimeOffset.UtcNow
        );

        _cookieWriter.Write(Response, cookie);

        return Ok(login);
    }

    // [HttpPost("login/guest")]
    // [AllowAnonymous]
    // public async Task<IActionResult> Create(
    //  [FromBody] CreateGuestRequest request,
    //  CancellationToken ct)
    // {
    //     var command = request.ToCommand();
    //     var guest = await _mediator.Send(command, ct);

    //     return Ok(guest);
    // }
}
using MediatR;
using BaitaHora.Api.Web.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YouDj.Api.Contracts.Auth;
using YouDj.Api.Helpers;
using YouDj.Api.Mappers;
using YouDj.Api.Mappers.Dj.Auth;
using YouDj.Api.Requests;
using YouDj.Application.Abstractions.Auth;
using YouDj.Application.Abstractions.Web;

namespace YouDj.Api.Controllers.Dj;

[ApiController]
[Route("api/dj/auth")]
public sealed class DjController : ControllerBase
{
    private readonly ISender _mediator;
    private readonly IJwtCookieFactory _cookieFactory;
    private readonly IJwtCookieWriter _cookieWriter;

    public DjController(
        IMediator mediator,
        IJwtCookieFactory cookieFactory,
        IJwtCookieWriter cookieWriter)
    {
        _mediator = mediator;
        _cookieFactory = cookieFactory;
        _cookieWriter = cookieWriter;
    }

    [HttpPost("register-dj")]
    public async Task<IActionResult> RegisterDj(
    [FromBody] RegisterDjRequest request,
    CancellationToken ct)
    {
        var command = request.ToCommand();
        var result = await _mediator.Send(command, ct);
        return result.ToActionResult(this);
    }

    [HttpPost("login")]
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

        return Ok(login.ToResponse());
    }

    [HttpGet("me")]
    [Authorize]
    public IActionResult Me(
        [FromServices] ICurrentDj currentDj)
    {
        if (!currentDj.IsAuthenticated)
            return Unauthorized();

        return Ok(new LoginResponse
        {
            DjId = currentDj.DjId,
            PlaylistId = currentDj.PlaylistId,
            ExpiresAtUtc = DateTime.UtcNow,
            IsDj = true
        });
    }
}
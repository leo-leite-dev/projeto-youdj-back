using BaitaHora.Api.Web.Cookies;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YouDj.Api.Contracts.Auth;
using YouDj.Api.Helpers;
using YouDj.Api.Mappers;
using YouDj.Api.Mappers.Auth;
using YouDj.Api.Requests;
using YouDj.Application.Abstractions.Auth;
using YouDj.Application.Abstractions.Web;
using YouDj.Application.Features.Auth.Login.Dj;

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


    [HttpPost("register-dj")]
    public async Task<IActionResult> RegisterDj(
    [FromBody] RegisterDjRequest request,
    CancellationToken ct)
    {
        var command = request.ToCommand();
        var result = await _mediator.Send(command, ct);
        return result.ToActionResult(this);
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
            ExpiresAtUtc = DateTime.UtcNow,
            IsDj = true
        });
    }
}
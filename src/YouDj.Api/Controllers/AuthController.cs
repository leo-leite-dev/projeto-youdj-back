using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YouDj.Api.Helpers;
using YouDj.Api.Mappers;
using YouDj.Api.Requests;
using YouDj.Application.Features.Auth.Login.Dj;

namespace YouDj.Api.Controllers.Auth;

[ApiController]
[Route("api/auth")]
public sealed class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login/dj")]
    public async Task<IActionResult> Login(
        LoginCommand command, CancellationToken ct)
    {
        var result = await _mediator.Send(command, ct);
        return result.ToActionResult(this);
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
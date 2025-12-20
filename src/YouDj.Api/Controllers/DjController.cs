using MediatR;
using Microsoft.AspNetCore.Mvc;
using YouDj.Api.Helpers;
using YouDj.Api.Mappers;
using YouDj.Api.Requests;

namespace YouDj.Api.Controllers;

[ApiController]
[Route("api/auth")]
public sealed class DjController : ControllerBase
{
    private readonly ISender _mediator;

    public DjController(ISender mediator)
    {
        _mediator = mediator;
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
}
using MediatR;
using Microsoft.AspNetCore.Mvc;
using YouDj.Api.Helpers;
using YouDj.Api.Contracts.Playlists;
using YouDj.Api.Mappers.Dj.Playlists;
using YouDj.Application.Features.Dj.Extension.Validate;

namespace YouDj.Api.Controllers.Dj;

[ApiController]
[Route("api/dj-extension")]
public sealed class DjExtensionController : ControllerBase
{
    private readonly ISender _mediator;

    public DjExtensionController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("{token}/queue/item")]
    public async Task<IActionResult> AddToQueue(
        string token,
        [FromBody] AddPlaylistItemRequest request,
        CancellationToken ct)
    {
        var validation = await _mediator.Send(
            new ValidateDjExtensionTokenQuery(token), ct);

        if (!validation.IsSuccess)
            return validation.ToActionResult(this);

        var context = validation.Value!;

        var guestId = Guid.NewGuid();

        var baseCommand = request.ToCommand(context.DjId);

        var command = baseCommand with
        {
            GuestId = guestId
        };

        var result = await _mediator.Send(command, ct);
        return result.ToActionResult(this);
    }
}

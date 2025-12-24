using MediatR;
using Microsoft.AspNetCore.Mvc;
using YouDj.Api.Helpers;
using YouDj.Application.Features.Youtube.Search;

namespace YouDj.Api.Controllers.Public;

[ApiController]
[Route("api/youtube")]
public sealed class YoutubeController : ControllerBase
{
    private readonly ISender _mediator;

    public YoutubeController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search(
        [FromQuery] string term,
        CancellationToken ct,
        [FromQuery] int limit = 10)
    {
        var query = new YoutubeSearchQuery(term, limit);
        var result = await _mediator.Send(query, ct);

        return result.ToActionResult(this);
    }
}
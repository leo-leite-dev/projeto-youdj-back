using MediatR;
using YouDj.Application.Common.Results;
using YouDj.Application.Interfaces.Youtube;
using YouDj.Application.Youtube;

namespace YouDj.Application.Features.Youtube.Search;

public sealed class YoutubeSearchHandler
    : IRequestHandler<YoutubeSearchQuery, Result<YoutubeSearchResultDto>>
{
    private readonly IYoutubeApiClient _youtubeApiClient;

    public YoutubeSearchHandler(IYoutubeApiClient youtubeApiClient)
    {
        _youtubeApiClient = youtubeApiClient;
    }

    public async Task<Result<YoutubeSearchResultDto>> Handle(
        YoutubeSearchQuery request, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(request.Term))
            return Result<YoutubeSearchResultDto>.BadRequest("Search term is required.");

        var result = await _youtubeApiClient.SearchAsync(request.Term, ct);

        return Result<YoutubeSearchResultDto>.Ok(result);
    }
}
using YouDj.Application.Youtube;

namespace YouDj.Application.Interfaces.Youtube;

public interface IYoutubeApiClient
{
    Task<YoutubeSearchResultDto> SearchAsync(string query, CancellationToken ct = default);
}
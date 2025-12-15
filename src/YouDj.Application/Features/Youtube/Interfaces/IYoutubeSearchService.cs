namespace YouDj.Application.Youtube;

public interface IYoutubeSearchService
{
    Task<YoutubeSearchResultDto> SearchAsync(string query, CancellationToken cancellationToken = default);
}
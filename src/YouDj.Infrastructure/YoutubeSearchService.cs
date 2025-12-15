using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;
using YouDj.Application.Interfaces.Youtube;
using YouDj.Application.Youtube;

namespace YouDj.Infrastructure.Youtube;

public class YoutubeApiClient : IYoutubeApiClient
{
    private readonly HttpClient _http;
    private readonly string _apiKey;

    public YoutubeApiClient(HttpClient httpClient, IConfiguration configuration)
    {
        _http = httpClient;
        _apiKey = configuration["Youtube:ApiKey"]
                  ?? throw new InvalidOperationException("Youtube:ApiKey not configured");
    }

    public async Task<YoutubeSearchResultDto> SearchAsync(string query, CancellationToken ct = default)
    {
        var url =
            "https://www.googleapis.com/youtube/v3/search" +
            $"?part=snippet&type=video&maxResults=10&q={Uri.EscapeDataString(query)}&key={_apiKey}";

        var response = await _http.GetFromJsonAsync<YoutubeSearchApiResponse>(url, ct);

        if (response?.Items is null)
            return new YoutubeSearchResultDto();

        return new YoutubeSearchResultDto
        {
            Items = response.Items
                .Where(i => i.Id?.VideoId is not null)
                .Select(i => new YoutubeVideoDto
                {
                    VideoId = i.Id!.VideoId!,
                    Title = i.Snippet!.Title ?? string.Empty,
                    ThumbnailUrl = i.Snippet.Thumbnails?.Default?.Url ?? string.Empty
                })
                .ToList()
        };
    }

    #region Response models (privados só pra deserializar)

    private class YoutubeSearchApiResponse
    {
        public List<Item>? Items { get; set; }
    }

    private class Item
    {
        public Id? Id { get; set; }
        public Snippet? Snippet { get; set; }
    }

    private class Id
    {
        public string? VideoId { get; set; }
    }

    private class Snippet
    {
        public string? Title { get; set; }
        public Thumbnails? Thumbnails { get; set; }
    }

    private class Thumbnails
    {
        public Thumbnail? Default { get; set; }
    }

    private class Thumbnail
    {
        public string? Url { get; set; }
    }

    #endregion
}

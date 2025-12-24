namespace YouDj.Api.Requests.Queue;

public sealed class CreateDjSongOrderRequest
{
    public Guid DjId { get; init; }

    public int PriceInCredits { get; init; }

    public string ExternalId { get; init; } = string.Empty;
    public string Title { get; init; } = string.Empty;
    public string ThumbnailUrl { get; init; } = string.Empty;
    public string Source { get; init; } = string.Empty;

    public TimeSpan? Duration { get; init; }
}

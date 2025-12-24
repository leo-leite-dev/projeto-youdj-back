namespace YouDj.Api.Contracts.Player;

public sealed record PlayNowRequest
{
    public required string ExternalId { get; init; }
    public required string Title { get; init; }
    public required string ThumbnailUrl { get; init; }
    public required string Source { get; init; }
}
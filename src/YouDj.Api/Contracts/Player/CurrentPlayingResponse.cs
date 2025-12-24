namespace YouDj.Api.Contracts.Player;

public sealed record CurrentPlayingResponse(
    string ExternalId,
    string Title,
    string ThumbnailUrl,
    string Source
);
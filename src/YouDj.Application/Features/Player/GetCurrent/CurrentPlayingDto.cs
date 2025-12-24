namespace YouDj.Application.Features.Player;

public sealed record CurrentPlayingDto(
    string ExternalId,
    string Title,
    string ThumbnailUrl,
    string Source
);
public sealed record AddMusicRequest(
    Guid DjId,
    string ExternalId,
    string Title,
    string ThumbnailUrl,
    int? DurationSeconds,
    string Source
);
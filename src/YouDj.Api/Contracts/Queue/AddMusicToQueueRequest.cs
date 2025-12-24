namespace YouDj.Api.Requests.Queue;

public sealed record AddMusicToQueueRequest(
    string ExternalId,
    string Title,
    string ThumbnailUrl,
    int? DurationSeconds,
    string Source
);
using YouDj.Application.Features.Queue;

namespace YouDj.Api.Mappers;

public static class AddMusicMapper
{
    public static AddMusicCommand ToCommand(this AddMusicRequest req)
        => new(
            req.DjId,
            req.ExternalId,
            req.Title,
            req.DurationSeconds is null
                ? null
                : TimeSpan.FromSeconds(req.DurationSeconds.Value),
            req.ThumbnailUrl,
            req.Source
        );
}
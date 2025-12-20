using YouDj.Api.Requests;
using YouDj.Application.Features.Queue.AddMusic;

namespace YouDj.Api.Mappers;

public static class QueueMapper
{
    public static AddMusicCommand ToCommand(this AddMusicRequest req)
        => new AddMusicCommand
        {
            DjId = req.DjId,
            ExternalId = req.ExternalId,
            Title = req.Title,
            Duration = req.DurationSeconds is null
                ? null
                : TimeSpan.FromSeconds(req.DurationSeconds.Value),
            ThumbnailUrl = req.ThumbnailUrl,
            Source = req.Source
        };
}
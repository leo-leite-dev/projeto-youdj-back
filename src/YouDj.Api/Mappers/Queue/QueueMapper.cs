using YouDj.Api.Requests;
using YouDj.Api.Requests.Queue;
using YouDj.Application.Features.Queue.AddMusicToQueue;
using YouDj.Application.Features.Queue.ReorderQueue;

namespace YouDj.Api.Mappers;

public static class QueueMapper
{
    public static AddMusicToQueueCommand ToCommand(
        this AddMusicToQueueRequest req,
        Guid djId)
        => new AddMusicToQueueCommand
        {
            DjId = djId,
            ExternalId = req.ExternalId,
            Title = req.Title,
            Duration = req.DurationSeconds is null
                ? null
                : TimeSpan.FromSeconds(req.DurationSeconds.Value),
            ThumbnailUrl = req.ThumbnailUrl,
            Source = req.Source
        };

    public static ReorderQueueCommand ToCommand(
          this ReorderQueueRequest request,
          Guid djId)
    {
        return new ReorderQueueCommand
        {
            DjId = djId,
            Items = request.Items
                .Select(x => new ReorderQueueItem(
                    x.QueueItemId,
                    x.Position))
                .ToList()
        };
    }
}
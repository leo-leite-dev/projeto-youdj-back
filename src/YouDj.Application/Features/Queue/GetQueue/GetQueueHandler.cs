using MediatR;
using YouDj.Application.Common.Results;
using YouDj.Application.Abstractions.Repositories;

namespace YouDj.Application.Features.Queue.GetQueue;

public sealed class GetQueueHandler
    : IRequestHandler<GetQueueQuery, Result<IReadOnlyList<QueueItemResponse>>>
{
    private readonly IQueueRepository _queueRepository;

    public GetQueueHandler(IQueueRepository queueRepository)
    {
        _queueRepository = queueRepository;
    }

    public async Task<Result<IReadOnlyList<QueueItemResponse>>> Handle(
        GetQueueQuery query, CancellationToken ct)
    {
        var items = await _queueRepository.GetByDjAsync(query.DjId, ct);

        var response = items
            .OrderBy(x => x.Position)
            .Select(x => new QueueItemResponse
            {
                Id = x.Id,
                Title = x.Track.Title,
                ThumbnailUrl = x.Track.ThumbnailUrl,
                Source = x.Track.Source,
                Position = x.Position
            })
            .ToList();

        return Result<IReadOnlyList<QueueItemResponse>>.Ok(response);
    }
}
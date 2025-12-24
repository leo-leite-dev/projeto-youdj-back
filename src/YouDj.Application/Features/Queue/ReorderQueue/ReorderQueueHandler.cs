using MediatR;
using YouDj.Application.Common.Results;
using YouDj.Application.Abstractions.Repositories;
using YouDj.Application.Abstractions.Persistences;
using YouDj.Domain.Queue;

namespace YouDj.Application.Features.Queue.ReorderQueue;

public sealed class ReorderQueueHandler
    : IRequestHandler<ReorderQueueCommand, Result>
{
    private readonly IQueueRepository _queueRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ReorderQueueHandler(
        IQueueRepository queueRepository,
        IUnitOfWork unitOfWork)
    {
        _queueRepository = queueRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(
        ReorderQueueCommand command, CancellationToken ct)
    {
        var queue = await _queueRepository
            .GetByDjWithTrackingAsync(command.DjId, ct);
            
        if (queue.Count == 0)
            return Result.Ok();

        var map = command.Items
            .ToDictionary(x => x.QueueItemId, x => x.Position);

        foreach (var item in queue)
        {
            if (item.Status != QueueStatus.Queued)
                continue;

            if (!map.TryGetValue(item.Id, out var newPosition))
                continue;

            item.SetPosition(newPosition);
        }

        await _unitOfWork.CommitAsync(ct);

        return Result.Ok();
    }
}
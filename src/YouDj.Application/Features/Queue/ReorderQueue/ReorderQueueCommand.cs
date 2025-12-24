using MediatR;
using YouDj.Application.Common.Results;

namespace YouDj.Application.Features.Queue.ReorderQueue;

public sealed record ReorderQueueCommand
    : IRequest<Result>
{
    public required Guid DjId { get; init; }
    public required IReadOnlyList<ReorderQueueItem> Items { get; init; }
}

public sealed record ReorderQueueItem(
    Guid QueueItemId,
    int Position
);
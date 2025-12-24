namespace YouDj.Api.Requests.Queue;

public sealed class ReorderQueueRequest
{
    public required IReadOnlyList<ReorderQueueItemRequest> Items { get; init; }
}

public sealed class ReorderQueueItemRequest
{
    public required Guid QueueItemId { get; init; }
    public required int Position { get; init; }
}
namespace YouDj.Application.Features.Queue.GetQueue;

public sealed record QueueItemResponse
{
    public Guid Id { get; init; }
    public string Title { get; init; } = default!;
    public string ThumbnailUrl { get; init; } = default!;
    public string Source { get; init; } = default!;
    public int Position { get; init; }
}
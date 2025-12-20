namespace YouDj.Domain.Queue;
public sealed class QueueItem
{
    public Guid Id { get; private set; }
    public Guid DjId { get; private set; }

    public string ExternalId { get; private set; }
    public string Title { get; private set; }
    public string ThumbnailUrl { get; private set; }
    public string Source { get; private set; }

    public TimeSpan? Duration { get; private set; }

    public QueueStatus Status { get; private set; }
    public int Position { get; private set; }

    public DateTime CreatedAt { get; private set; }

    private QueueItem() { }  

    public QueueItem(
        Guid djId,
        string externalId,
        string title,
        string thumbnailUrl,
        string source,
        TimeSpan? duration,
        int position)
    {
        Id = Guid.NewGuid();
        DjId = djId;
        ExternalId = externalId;
        Title = title;
        ThumbnailUrl = thumbnailUrl;
        Source = source;
        Duration = duration;
        Position = position;
        Status = QueueStatus.Queued;
        CreatedAt = DateTime.UtcNow;
    }

    public void MarkAsPlaying()
    {
        if (Status != QueueStatus.Queued)
            throw new InvalidOperationException("Item não está na fila.");

        Status = QueueStatus.Playing;
    }

    public void MarkAsPlayed()
    {
        if (Status != QueueStatus.Playing)
            throw new InvalidOperationException("Item não está tocando.");

        Status = QueueStatus.Played;
    }
}
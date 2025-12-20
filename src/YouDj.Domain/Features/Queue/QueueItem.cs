using YouDj.Domain.Features.Common;
using YouDj.Domain.Features.Queue.Exceptions;

namespace YouDj.Domain.Queue;

public sealed class QueueItem : EntityBase
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


    private QueueItem() { }

    private QueueItem(Guid djId, string externalId, string title,
        string thumbnailUrl, string source, TimeSpan? duration)
    {
        Id = Guid.NewGuid();
        DjId = djId;
        ExternalId = externalId;
        Title = title;
        ThumbnailUrl = thumbnailUrl;
        Source = source;
        Duration = duration;

        Status = QueueStatus.Queued;
        Position = 0;
    }

    public static QueueItem Create(Guid djId, string externalId, string title,
        string thumbnailUrl, string source, TimeSpan? duration)
    {
        if (djId == Guid.Empty)
            throw new QueueException("DJ inválido.");

        if (string.IsNullOrWhiteSpace(externalId))
            throw new QueueException("ExternalId é obrigatório.");

        if (string.IsNullOrWhiteSpace(title))
            throw new QueueException("Título é obrigatório.");

        if (string.IsNullOrWhiteSpace(source))
            throw new QueueException("Fonte é obrigatória.");

        return new QueueItem(
            djId,
            externalId,
            title,
            thumbnailUrl,
            source,
            duration);
    }

    public void SetPosition(int position)
    {
        if (position < 0)
            throw new QueueException("Posição inválida.");

        Position = position;
    }

    public void MarkAsPlaying()
    {
        if (Status != QueueStatus.Queued)
            throw new QueueException("Item não está na fila.");

        Status = QueueStatus.Playing;
    }

    public void MarkAsPlayed()
    {
        if (Status != QueueStatus.Playing)
            throw new QueueException("Item não está tocando.");

        Status = QueueStatus.Played;
    }
}
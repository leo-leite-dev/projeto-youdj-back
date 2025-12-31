using YouDj.Domain.Features.Common;
using YouDj.Domain.Features.Common.ValueObjects;
using YouDj.Domain.Features.Queue.Exceptions;
using YouDj.Domain.SongOrders;

namespace YouDj.Domain.Queue;

public sealed class QueueItem : EntityBase
{
    public Guid DjId { get; private set; }

    public Guid GuestId { get; private set; }

    public int PriceInCredits { get; private set; }

    public TrackInfo Track { get; private set; } = default!;
    public TimeSpan? Duration { get; private set; }

    public QueueStatus Status { get; private set; }
    public int Position { get; private set; }

    private QueueItem() { }

    private QueueItem(Guid djId, Guid guestId, int priceInCredits,
        TrackInfo track, TimeSpan? duration)
    {
        DjId = djId;
        GuestId = guestId;
        PriceInCredits = priceInCredits;
        Track = track;
        Duration = duration;
        Status = QueueStatus.Queued;
        Position = 0;
    }

    public static QueueItem CreateByDj(
        Guid djId, TrackInfo track, TimeSpan? duration)
    {
        if (djId == Guid.Empty)
            throw new QueueException("DJ inválido.");

        if (track is null)
            throw new QueueException("TrackInfo é obrigatório.");

        return CreateInternal(djId, Guid.Empty, 0, track, duration);
    }

    public static QueueItem CreateByExtension(
        Guid djId, Guid extensionGuestId,
        TrackInfo track, TimeSpan? duration)
    {
        if (djId == Guid.Empty)
            throw new QueueException("DJ inválido.");

        if (extensionGuestId == Guid.Empty)
            throw new QueueException("Guest inválido.");

        return CreateInternal(djId, extensionGuestId, 0, track, duration);
    }

    // 💳 Convidado comum (pedido pago / futuro)
    public static QueueItem CreateFromOrder(DjSongOrder order)
    {
        if (order is null)
            throw new QueueException("Order inválida.");

        if (order.DjId == Guid.Empty)
            throw new QueueException("DJ inválido na order.");

        if (string.IsNullOrWhiteSpace(order.ExternalId))
            throw new QueueException("Order sem ExternalId.");

        if (string.IsNullOrWhiteSpace(order.Title))
            throw new QueueException("Order sem Title.");

        if (string.IsNullOrWhiteSpace(order.Source))
            throw new QueueException("Order sem Source.");

        if (string.IsNullOrWhiteSpace(order.ThumbnailUrl))
            throw new QueueException("Order sem Thumbnail.");

        if (order.PriceInCredits <= 0)
            throw new QueueException("Order sem custo.");

        var track = TrackInfo.Create(
            order.ExternalId,
            order.Title,
            order.ThumbnailUrl,
            order.Source
        );

        return CreateInternal(
            order.DjId,
            Guid.Empty,
            order.PriceInCredits,
            track,
            order.Duration
        );
    }

    private static QueueItem CreateInternal(Guid djId, Guid guestId,
        int priceInCredits, TrackInfo track, TimeSpan? duration)
    {
        if (guestId == Guid.Empty && priceInCredits < 0)
            throw new QueueException("Créditos inválidos.");

        if (guestId != Guid.Empty && priceInCredits > 0)
            throw new QueueException("Convidado especial não usa créditos.");

        return new QueueItem(djId, guestId, priceInCredits, track, duration);
    }

    public void SetPosition(int position)
    {
        if (position < 0)
            throw new QueueException("Posição inválida.");

        Position = position;
        Touch();
    }

    public void MarkAsPlaying()
    {
        if (Status != QueueStatus.Queued)
            throw new QueueException("Item não está na fila.");

        Status = QueueStatus.Playing;
        Touch();
    }

    public void MarkAsPlayed()
    {
        if (Status != QueueStatus.Playing)
            throw new QueueException("Item não está tocando.");

        Status = QueueStatus.Played;
        Touch();
    }
}
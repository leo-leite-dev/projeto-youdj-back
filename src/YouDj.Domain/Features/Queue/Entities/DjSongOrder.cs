using YouDj.Domain.Features.Common;
using YouDj.Domain.Features.Common.Enums;
using YouDj.Domain.SongOrders.Exceptions;

namespace YouDj.Domain.SongOrders;

public sealed class DjSongOrder : EntityBase
{
    public Guid DjId { get; private set; }
    public Guid GuestId { get; private set; }

    public int PriceInCredits { get; private set; }

    public string ExternalId { get; private set; } = string.Empty;
    public string Title { get; private set; } = string.Empty;
    public string ThumbnailUrl { get; private set; } = string.Empty;
    public string Source { get; private set; } = string.Empty;

    public TimeSpan? Duration { get; private set; }

    public DjSongOrderStatus Status { get; private set; }

    private DjSongOrder() { }

    private DjSongOrder(Guid djId, Guid guestId, int priceInCredits, string externalId,
        string title, string thumbnailUrl, string source, TimeSpan? duration)
    {
        DjId = djId;
        GuestId = guestId;
        PriceInCredits = priceInCredits;
        ExternalId = externalId;
        Title = title;
        ThumbnailUrl = thumbnailUrl;
        Source = source;
        Duration = duration;
        Status = DjSongOrderStatus.Pending;
    }

    public static DjSongOrder Create(Guid djId, Guid guestId, int priceInCredits,
        string externalId, string title, string thumbnailUrl, string source,
        TimeSpan? duration)
    {
        if (djId == Guid.Empty)
            throw DjSongOrderException.InvalidDj();

        if (guestId == Guid.Empty)
            throw DjSongOrderException.InvalidGuest();

        if (priceInCredits <= 0)
            throw DjSongOrderException.InvalidPrice();

        if (string.IsNullOrWhiteSpace(externalId))
            throw DjSongOrderException.ExternalIdRequired();

        if (string.IsNullOrWhiteSpace(title))
            throw DjSongOrderException.TitleRequired();

        if (string.IsNullOrWhiteSpace(source))
            throw DjSongOrderException.SourceRequired();

        if (string.IsNullOrWhiteSpace(thumbnailUrl))
            throw DjSongOrderException.ThumbnailRequired();

        return new DjSongOrder(djId, guestId, priceInCredits, externalId, title,
            thumbnailUrl, source, duration);
    }

    public void Accept()
    {
        if (Status != DjSongOrderStatus.Pending)
            throw DjSongOrderException.NotPending();

        Status = DjSongOrderStatus.Accepted;
        Touch();
    }

    public void Reject()
    {
        if (Status != DjSongOrderStatus.Pending)
            throw DjSongOrderException.NotPending();

        Status = DjSongOrderStatus.Rejected;
        Touch();
    }
}
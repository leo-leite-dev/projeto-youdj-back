using YouDj.Domain.Features.Common;
using YouDj.Domain.Features.Common.ValueObjects;

namespace YouDj.Domain.Player;

public sealed class NowPlaying : EntityBase
{
    public Guid DjId { get; private set; }

    public TrackInfo Track { get; private set; } = default!;

    public DateTime StartedAt { get; private set; }

    private NowPlaying() { }

    private NowPlaying(Guid djId, TrackInfo track)
    {
        DjId = djId;
        Track = track;
        StartedAt = DateTime.UtcNow;
    }

    public static NowPlaying Start(Guid djId, TrackInfo track)
    {
        if (djId == Guid.Empty)
            throw new ArgumentException("DjId inválido.");

        if (track is null)
            throw new ArgumentNullException(nameof(track), "TrackInfo é obrigatório.");

        return new NowPlaying(djId, track);
    }

    public void Restart()
    {
        StartedAt = DateTime.UtcNow;
        Touch();
    }
}
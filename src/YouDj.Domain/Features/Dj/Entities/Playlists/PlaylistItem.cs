using YouDj.Domain.Features.Common;
using YouDj.Domain.Features.Common.ValueObjects;

namespace YouDj.Domain.Features.Dj.Entities.Playlists;

public sealed class PlaylistItem : EntityBase
{
    public Guid PlaylistId { get; private set; }
    public Guid? FolderId { get; private set; }

    public TrackInfo Track { get; private set; } = default!;

    public int Position { get; private set; }

    private PlaylistItem() { }

    public static PlaylistItem Create(Guid playlistId, TrackInfo track,
        Guid? folderId, int position)
    {
        return new PlaylistItem
        {
            PlaylistId = playlistId,
            FolderId = folderId,
            Track = track,
            Position = position
        };
    }

    public void MoveToFolder(Guid? folderId)
    {
        FolderId = folderId;
        Touch();
    }

    public void SetPosition(int position)
    {
        Position = position;
        Touch();
    }
}
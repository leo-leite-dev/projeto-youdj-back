using YouDj.Domain.Features.Common;
using YouDj.Domain.Playlists.Exceptions;

namespace YouDj.Domain.Features.Dj.Entities.Playlists;

public sealed class PlaylistFolder : EntityBase
{
    public Guid PlaylistId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public int Position { get; private set; }

    private PlaylistFolder() { }

    public static PlaylistFolder Create(Guid playlistId, string name, int position)
    {
        if (playlistId == Guid.Empty)
            throw new ArgumentException("Playlist inválida.", nameof(playlistId));

        if (string.IsNullOrWhiteSpace(name))
            throw PlaylistException.NameRequired();

        if (position < 0)
            throw new ArgumentOutOfRangeException(nameof(position));

        return new PlaylistFolder
        {
            PlaylistId = playlistId,
            Name = name,
            Position = position
        };
    }

    public void Rename(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw PlaylistException.NameRequired();

        if (Name == name)
            return;

        Name = name;
        Touch();
    }

    public void SetPosition(int position)
    {
        if (position < 0)
            throw new ArgumentOutOfRangeException(nameof(position));

        if (Position == position)
            return;

        Position = position;
        Touch();
    }
}
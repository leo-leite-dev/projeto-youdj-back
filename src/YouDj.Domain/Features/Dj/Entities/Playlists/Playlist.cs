using YouDj.Domain.Features.Common;

namespace YouDj.Domain.Features.Dj.Entities.Playlists;

public sealed class Playlist : EntityBase
{
    public Guid DjId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string PublicSlug { get; private set; } = string.Empty;
    public string PublicToken { get; private set; } = string.Empty;

    private Playlist() { }

    public static Playlist Create(Guid djId, string djUsername)
    {
        var playlist = new Playlist
        {
            DjId = djId,
            Name = $"Playlist de {djUsername}",
            PublicSlug = GenerateSlug(djUsername)
        };

        playlist.GeneratePublicToken();
        return playlist;
    }

    private static string GenerateSlug(string username)
    {
        var shortId = Guid.NewGuid().ToString("N")[..8];
        return $"{username.ToLowerInvariant()}-{shortId}";
    }

    public void GeneratePublicToken()
    {
        PublicToken = Guid.NewGuid().ToString("N");
        Touch();
    }
}
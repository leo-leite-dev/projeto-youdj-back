using YouDj.Api.Contracts.Playlists;
using YouDj.Application.Features.Dj.Playlists.Add;

namespace YouDj.Api.Mappers.Dj.Playlists;

public static class PlaylistItemMapper
{
    public static AddMusicToPlaylistCommand ToCommand(
        this AddPlaylistItemRequest request,
        Guid djId)
    {
        return new AddMusicToPlaylistCommand
        {
            DjId = djId,
            PlaylistId = request.PlaylistId,
            FolderId = request.FolderId,
            ExternalId = request.ExternalId,
            Title = request.Title,
            ThumbnailUrl = request.ThumbnailUrl,
            Source = request.Source
        };
    }
}
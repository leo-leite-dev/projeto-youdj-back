using YouDj.Api.Contracts.Playlists;
using YouDj.Application.Features.Dj.Playlists.Folder.Create;
using YouDj.Application.Features.Dj.Playlists.Folder.Get;

namespace YouDj.Api.Mappers.Dj.Playlists;

public static class PlaylistFolderMapper
{
    public static CreatePlaylistFolderCommand ToCommand(
        this CreatePlaylistFolderRequest request,
        Guid djId)
    {
        return new CreatePlaylistFolderCommand
        {
            DjId = djId,
            PlaylistId = request.PlaylistId,
            Name = request.Name
        };
    }

    public static GetPlaylistFoldersQuery ToQuery(
        this GetPlaylistFoldersRequest request,
        Guid djId)
    {
        return new GetPlaylistFoldersQuery
        {
            DjId = djId,
            PlaylistId = request.PlaylistId
        };
    }
}
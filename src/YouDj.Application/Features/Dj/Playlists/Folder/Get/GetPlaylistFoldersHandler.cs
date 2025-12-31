using MediatR;
using YouDj.Application.Abstractions.Repositories.Dj.Playlists;
using YouDj.Application.Common.Results;

namespace YouDj.Application.Features.Dj.Playlists.Folder.Get;

public sealed class GetPlaylistFoldersHandler
    : IRequestHandler<GetPlaylistFoldersQuery, Result<IReadOnlyList<PlaylistFolderDto>>>
{
    private readonly IPlaylistRepository _playlistRepository;
    private readonly IPlaylistFolderRepository _folderRepository;

    public GetPlaylistFoldersHandler(
        IPlaylistRepository playlistRepository,
        IPlaylistFolderRepository folderRepository)
    {
        _playlistRepository = playlistRepository;
        _folderRepository = folderRepository;
    }

    public async Task<Result<IReadOnlyList<PlaylistFolderDto>>> Handle(
        GetPlaylistFoldersQuery query, CancellationToken ct)
    {
        var playlist = await _playlistRepository
            .GetByIdAsync(query.PlaylistId, ct);

        if (playlist is null || playlist.DjId != query.DjId)
            return Result<IReadOnlyList<PlaylistFolderDto>>
                .NotFound("Playlist não encontrada.");

        var folders = await _folderRepository
            .GetByPlaylistAsync(playlist.Id, ct);

        var result = folders
            .Select(x => new PlaylistFolderDto(
                x.Id,
                x.Name,
                x.Position))
            .ToList();

        return Result<IReadOnlyList<PlaylistFolderDto>>.Ok(result);
    }
}
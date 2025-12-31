using MediatR;
using YouDj.Application.Common.Results;
using YouDj.Application.Abstractions.Persistences;
using YouDj.Application.Abstractions.Repositories.Dj.Playlists;
using YouDj.Domain.Features.Dj.Entities.Playlists;

namespace YouDj.Application.Features.Dj.Playlists.Folder.Create;

public sealed class CreatePlaylistFolderHandler
    : IRequestHandler<CreatePlaylistFolderCommand, Result<Guid>>
{
    private readonly IPlaylistRepository _playlistRepository;
    private readonly IPlaylistFolderRepository _folderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreatePlaylistFolderHandler(
        IPlaylistRepository playlistRepository,
        IPlaylistFolderRepository folderRepository,
        IUnitOfWork unitOfWork)
    {
        _playlistRepository = playlistRepository;
        _folderRepository = folderRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(
        CreatePlaylistFolderCommand command, CancellationToken ct)
    {
        var playlist = await _playlistRepository
            .GetByIdAsync(command.PlaylistId, ct);

        if (playlist is null || playlist.DjId != command.DjId)
            return Result<Guid>.NotFound("Playlist não encontrada.");

        var lastPosition =
            await _folderRepository
                .GetLastPositionAsync(playlist.Id, ct);

        var folder = PlaylistFolder.Create(
        playlistId: playlist.Id,
        name: command.Name,
        position: lastPosition + 1
        );

        await _folderRepository.AddAsync(folder, ct);
        await _unitOfWork.CommitAsync(ct);

        return Result<Guid>.Ok(folder.Id);
    }
}
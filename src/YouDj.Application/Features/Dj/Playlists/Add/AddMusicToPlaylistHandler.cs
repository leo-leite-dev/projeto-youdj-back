using MediatR;
using YouDj.Application.Common.Results;
using YouDj.Application.Abstractions.Persistences;
using YouDj.Domain.Features.Common.ValueObjects;
using YouDj.Domain.Playlists.Exceptions;
using YouDj.Application.Abstractions.Repositories.Dj.Playlists;
using YouDj.Domain.Features.Dj.Entities.Playlists;

namespace YouDj.Application.Features.Dj.Playlists.Add;

public sealed class AddMusicToPlaylistHandler
    : IRequestHandler<AddMusicToPlaylistCommand, Result<Guid>>
{
    private readonly IPlaylistRepository _playlistRepository;
    private readonly IPlaylistItemRepository _playlistItemRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddMusicToPlaylistHandler(
        IPlaylistRepository playlistRepository,
        IPlaylistItemRepository playlistItemRepository,
        IUnitOfWork unitOfWork)
    {
        _playlistRepository = playlistRepository;
        _playlistItemRepository = playlistItemRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(
        AddMusicToPlaylistCommand command, CancellationToken ct)
    {
        var playlist = await _playlistRepository
            .GetByIdAsync(command.PlaylistId, ct);

        if (playlist is null || playlist.DjId != command.DjId)
            return Result<Guid>.NotFound("Playlist não encontrada.");

        var alreadyExists =
            await _playlistItemRepository
                .ExistsAsync(
                    playlist.Id,
                    command.ExternalId,
                    ct);

        if (alreadyExists)
            return Result<Guid>.BadRequest(
                PlaylistException.ItemAlreadyAdded().Message);

        var lastPosition =
            await _playlistItemRepository
                .GetLastPositionAsync(
                    playlist.Id,
                    command.FolderId,
                    ct);

        var track = TrackInfo.Create(
            command.ExternalId,
            command.Title,
            command.ThumbnailUrl,
            command.Source
        );

        var item = PlaylistItem.Create(
            playlist.Id,
            track,
            command.FolderId,
            lastPosition + 1
        );

        await _playlistItemRepository.AddAsync(item, ct);
        await _unitOfWork.CommitAsync(ct);

        return Result<Guid>.Ok(item.Id);
    }
}
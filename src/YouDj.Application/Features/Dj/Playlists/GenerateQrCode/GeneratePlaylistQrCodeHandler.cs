using MediatR;
using YouDj.Application.Abstractions.Auth;
using YouDj.Application.Abstractions.Repositories;
using YouDj.Application.Abstractions.Repositories.Dj.Playlists;
using YouDj.Application.Common.Results;

namespace YouDj.Application.Features.Playlists.GenerateQrCode;

public sealed class GeneratePlaylistQrCodeHandler
    : IRequestHandler<GeneratePlaylistQrCodeCommand, Result<GeneratePlaylistQrCodeResult>>
{
    private readonly IDjRepository _djRepository;
    private readonly IPlaylistRepository _playlistRepository;
    private readonly ICurrentDj _currentDj;

    public GeneratePlaylistQrCodeHandler(
        IDjRepository djRepository,
        IPlaylistRepository playlistRepository,
        ICurrentDj currentDj)
    {
        _djRepository = djRepository;
        _playlistRepository = playlistRepository;
        _currentDj = currentDj;
    }

    public async Task<Result<GeneratePlaylistQrCodeResult>> Handle(
        GeneratePlaylistQrCodeCommand _, CancellationToken ct)
    {
        var userId = _currentDj.DjId;

        var user = await _djRepository.GetByIdAsync(userId, ct);
        if (user is null || user.ActivePlaylistId is null)
            return Result<GeneratePlaylistQrCodeResult>.NotFound("Playlist ativa não encontrada.");

        var playlist = await _playlistRepository.GetByIdAsync(
            user.ActivePlaylistId.Value, ct);

        if (playlist is null)
            return Result<GeneratePlaylistQrCodeResult>.NotFound("Playlist não encontrada.");

        if (string.IsNullOrEmpty(playlist.PublicToken))
        {
            playlist.GeneratePublicToken();
            await _playlistRepository.UpdateAsync(playlist, ct);
        }

        var url = $"https://app.youdj.com/p/{playlist.PublicToken}";

        return Result<GeneratePlaylistQrCodeResult>.Ok(
            new GeneratePlaylistQrCodeResult
            {
                PlaylistUrl = url
            });
    }
}
using MediatR;
using YouDj.Application.Abstractions.Auth;
using YouDj.Application.Abstractions.Repositories;
using YouDj.Application.Common.Results;

namespace YouDj.Application.Features.Playlists.GenerateQrCode;

public sealed class GeneratePlaylistQrCodeHandler
    : IRequestHandler<GeneratePlaylistQrCodeCommand, Result<GeneratePlaylistQrCodeResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPlaylistRepository _playlistRepository;
    private readonly ICurrentUser _currentUser;

    public GeneratePlaylistQrCodeHandler(
        IUserRepository userRepository,
        IPlaylistRepository playlistRepository,
        ICurrentUser currentUser)
    {
        _userRepository = userRepository;
        _playlistRepository = playlistRepository;
        _currentUser = currentUser;
    }

    public async Task<Result<GeneratePlaylistQrCodeResult>> Handle(
        GeneratePlaylistQrCodeCommand _, CancellationToken ct)
    {
        var userId = _currentUser.UserId;

        var user = await _userRepository.GetByIdAsync(userId, ct);
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
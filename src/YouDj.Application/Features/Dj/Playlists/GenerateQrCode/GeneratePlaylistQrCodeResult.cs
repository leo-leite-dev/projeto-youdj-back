namespace YouDj.Application.Features.Playlists.GenerateQrCode;

public sealed record GeneratePlaylistQrCodeResult
{
    public string PlaylistUrl { get; init; } = string.Empty;
}

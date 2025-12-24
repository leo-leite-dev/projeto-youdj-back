using YouDj.Application.Features.Auth.PlaylistSession;

namespace YouDj.Application.Abstractions.Auth;

public interface IPlaylistSessionTokenService
{
    Task<PlaylistSessionResult> IssueAsync(string displayName, string playlistToken);
}
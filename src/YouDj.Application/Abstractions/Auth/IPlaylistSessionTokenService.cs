using YouDj.Application.Features.Dj.Auth.Session;

namespace YouDj.Application.Abstractions.Auth;

public interface IPlaylistSessionTokenService
{
    Task<PlaylistSessionResult> IssueAsync(string displayName, string playlistToken);
}
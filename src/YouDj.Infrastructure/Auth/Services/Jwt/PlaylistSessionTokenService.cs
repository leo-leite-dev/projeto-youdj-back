using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using YouDj.Application.Abstractions.Auth;
using YouDj.Application.Abstractions.Repositories.Dj.Playlists;
using YouDj.Application.Features.Dj.Auth.Session;
using YouDj.Domain.Features.Common.Exceptions;

namespace YouDj.Infrastructure.Auth.Services.Jwt;

public sealed class PlaylistSessionTokenService : IPlaylistSessionTokenService
{
    private readonly IPlaylistRepository _playlistRepository;
    private readonly JwtOptions _options;

    public PlaylistSessionTokenService(
        IPlaylistRepository playlistRepository,
        IOptions<JwtOptions> options)
    {
        _playlistRepository = playlistRepository;
        _options = options.Value;
    }

    public async Task<PlaylistSessionResult> IssueAsync(
        string displayName,
        string playlistToken)
    {
        var playlist = await _playlistRepository
            .GetByPublicTokenAsync(playlistToken);

        if (playlist is null)
            throw new DjException("Playlist não encontrada.");

        return IssueInternal(
            displayName,
            playlist.Id,
            playlist.DjId);
    }

    private PlaylistSessionResult IssueInternal(
        string displayName,
        Guid playlistId,
        Guid djId)
    {
        var now = DateTime.UtcNow;
        var expiresAt = now.AddHours(2);

        var claims = new List<Claim>
        {
            new("scope", "playlist-session"),
            new("playlist_id", playlistId.ToString()),
            new("dj_id", djId.ToString()),
            new("display_name", displayName)
        };

        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_options.Secret)),
            SecurityAlgorithms.HmacSha256
        );

        var jwt = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: claims,
            notBefore: now,
            expires: expiresAt,
            signingCredentials: credentials
        );

        var token = new JwtSecurityTokenHandler().WriteToken(jwt);

        return new PlaylistSessionResult
        {
            AccessToken = token,
            ExpiresAtUtc = expiresAt,
            PlaylistId = playlistId,
            DjId = djId,
            DisplayName = displayName
        };
    }
}
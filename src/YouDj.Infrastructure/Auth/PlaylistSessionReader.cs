using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using YouDj.Application.Abstractions.Auth;
using YouDj.Application.Features.Auth.Session;

namespace YouDj.Infrastructure.Auth;

public sealed class PlaylistSessionReader : IPlaylistSessionReader
{
    private readonly JwtOptions _options;

    public PlaylistSessionReader(IOptions<JwtOptions> options)
    {
        _options = options.Value;
    }

    public PlaylistSessionData Read(string sessionToken)
    {
        if (string.IsNullOrWhiteSpace(sessionToken))
            throw new InvalidOperationException("Session token ausente.");

        var handler = new JwtSecurityTokenHandler();

        var parameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,

            ValidIssuer = _options.Issuer,
            ValidAudience = _options.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_options.Secret)
            ),

            ClockSkew = TimeSpan.Zero
        };

        var principal = handler.ValidateToken(
            sessionToken,
            parameters,
            out _
        );

        var scope = principal.FindFirstValue("scope");
        if (scope != "playlist-session")
            throw new InvalidOperationException(
                "Token inválido para sessão de playlist."
            );

        var displayName = principal.FindFirstValue("display_name");
        var djIdRaw = principal.FindFirstValue("dj_id");
        var playlistIdRaw = principal.FindFirstValue("playlist_id");

        if (displayName is null || djIdRaw is null || playlistIdRaw is null)
            throw new InvalidOperationException(
                "Token de playlist inválido."
            );

        return new PlaylistSessionData(
            displayName,
            Guid.Parse(djIdRaw),
            Guid.Parse(playlistIdRaw)
        );
    }
}
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using YouDj.Application.Abstractions.Auth;
using YouDj.Application.Features.Auth.Session;

namespace YouDj.Infrastructure.Auth.Services.Jwt;

public sealed class GuestSessionTokenService : IGuestSessionTokenService
{
    private readonly JwtOptions _options;

    public GuestSessionTokenService(IOptions<JwtOptions> options)
    {
        _options = options.Value;
    }

    public GuestSessionResult Issue(
        string displayName,
        Guid djId,
        Guid? playlistId = null)
    {
        var now = DateTime.UtcNow;
        var expiresAt = now.AddMinutes(30);

        var claims = new List<Claim>
        {
            new("scope", "guest-session"),
            new("display_name", displayName),
            new("dj_id", djId.ToString())
        };

        if (playlistId.HasValue)
        {
            claims.Add(
                new Claim("playlist_id", playlistId.Value.ToString())
            );
        }

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

        return new GuestSessionResult
        {
            DisplayName = displayName,
            DjId = djId,
            AccessToken = token,
            ExpiresAtUtc = expiresAt
        };
    }
}
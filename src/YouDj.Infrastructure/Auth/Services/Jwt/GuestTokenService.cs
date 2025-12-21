using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using YouDj.Application.Abstractions.Auth;
using YouDj.Application.Features.Auth.Guest;

namespace YouDj.Infrastructure.Auth.Guest;

public sealed class GuestTokenService : IGuestTokenService
{
    private readonly JwtOptions _options;

    public GuestTokenService(IOptions<JwtOptions> options)
    {
        _options = options.Value;
    }

    public GuestTokenResult Issue(Guid guestId)
    {
        var now = DateTime.UtcNow;
        var expiresAt = now.AddDays(30);

        var claims = new List<Claim>
        {
            new("guest_id", guestId.ToString()),
            new("scope", "guest")
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

        return new GuestTokenResult(
            AccessToken: token,
            ExpiresAtUtc: expiresAt
        );
    }
}
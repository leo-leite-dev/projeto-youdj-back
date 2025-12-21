using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using YouDj.Application.Abstractions.Auth;
using YouDj.Application.Features.Auth.Session;

public sealed class GuestSessionTokenService : IGuestSessionTokenService
{
    private readonly JwtOptions _options;

    public GuestSessionTokenService(IOptions<JwtOptions> options)
    {
        _options = options.Value;
    }

    public GuestSessionResult Issue(string displayName, Guid djId)
    {
        var now = DateTime.UtcNow;
        var expiresAt = now.AddHours(2);

        var claims = new List<Claim>
        {
            new("display_name", displayName),
            new("dj_id", djId.ToString()),
            new("scope", "session")
        };

        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_options.Secret)),
            SecurityAlgorithms.HmacSha256
        );

        var jwt = new JwtSecurityToken(
            _options.Issuer,
            _options.Audience,
            claims,
            now,
            expiresAt,
            credentials
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
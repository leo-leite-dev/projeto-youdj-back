using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using YouDj.Application.Abstractions.Auth;
using YouDj.Application.Features.Auth;
using YouDj.Domain.Features.Common.ValueObjects;

namespace YouDj.Infrastructure.Auth.Services.Jwt;

public sealed class JwtTokenService : IJwtTokenService
{
    private readonly JwtOptions _options;
    private readonly ILogger<JwtTokenService> _logger;

    public JwtTokenService(
        IOptions<JwtOptions> options,
        ILogger<JwtTokenService> logger)
    {
        _options = options.Value;
        _logger = logger;
    }

    public Task<AuthResult> IssueAsync(
        Guid userId,
        Username username,
        IEnumerable<string> roles,
        IDictionary<string, string>? extraClaims = null,
        CancellationToken ct = default)
    {
        var now = DateTime.UtcNow;
        var expiresAt = now.AddMinutes(_options.AccessTokenMinutes);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new(JwtRegisteredClaimNames.UniqueName, username.Value),
            new(ClaimTypes.NameIdentifier, userId.ToString()),
            new(ClaimTypes.Name, username.Value)
        };

        foreach (var role in roles)
            claims.Add(new Claim(ClaimTypes.Role, role));

        if (extraClaims is not null)
        {
            foreach (var (key, value) in extraClaims)
            {
                if (claims.Any(c => c.Type == key))
                    continue;

                claims.Add(new Claim(key, value));
            }
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

        return Task.FromResult(new AuthResult(
            AccessToken: token,
            ExpiresAtUtc: expiresAt,
            DjId: userId,
            Username: username.Value
        ));
    }

    public AuthTokenValidationResult Validate(string token)
    {
        try
        {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_options.Secret);

            var principal = handler.ValidateToken(
                token,
                new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = _options.Issuer,
                    ValidateAudience = true,
                    ValidAudience = _options.Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(1)
                },
                out _);

            var userIdRaw =
                principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var username =
                principal.FindFirst(ClaimTypes.Name)?.Value;

            return new AuthTokenValidationResult(
                IsValid: Guid.TryParse(userIdRaw, out var uid),
                UserId: uid,
                Username: username,
                Roles: principal
                    .FindAll(ClaimTypes.Role)
                    .Select(r => r.Value)
                    .ToList(),
                Claims: principal.Claims
                    .ToDictionary(c => c.Type, c => c.Value)
            );
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "[JwtTokenService] Token inválido.");

            return new AuthTokenValidationResult(
                IsValid: false,
                UserId: null,
                Username: null,
                Roles: Array.Empty<string>(),
                Claims: new Dictionary<string, string>()
            );
        }
    }
}
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using YouDj.Application.Abstractions.Auth;

namespace YouDj.Infrastructure.Auth;

public sealed class GuestSessionReader : IGuestSessionReader
{
    private readonly JwtOptions _options;

    public GuestSessionReader(IOptions<JwtOptions> options)
    {
        _options = options.Value;
    }

    public GuestSessionData Read(string sessionToken)
    {
        if (string.IsNullOrWhiteSpace(sessionToken))
            throw new InvalidOperationException("Session token ausente.");

        var tokenHandler = new JwtSecurityTokenHandler();

        var validationParameters = new TokenValidationParameters
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

        var principal = tokenHandler.ValidateToken(
            sessionToken,
            validationParameters,
            out _
        );

        var scope = principal.FindFirstValue("scope");
        if (scope != "guest-session")
            throw new InvalidOperationException(
                "Token inválido para sessão de pagamento."
            );

        var displayName = principal.FindFirstValue("display_name");
        var djIdRaw = principal.FindFirstValue("dj_id");

        if (displayName is null || djIdRaw is null)
            throw new InvalidOperationException(
                "Token de sessão de pagamento inválido."
            );

        return new GuestSessionData(
            displayName,
            Guid.Parse(djIdRaw)
        );
    }
}
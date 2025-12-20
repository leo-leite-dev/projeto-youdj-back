using YouDj.Domain.Features.Common.ValueObjects;
using YouDj.Application.Features.Auth;

namespace YouDj.Application.Abstractions.Auth;

public interface IJwtTokenService
{
    Task<AuthResult> IssueAsync(Guid userId, Username username, IEnumerable<string> roles, IDictionary<string, string>? extraClaims = null, CancellationToken ct = default);
    AuthTokenValidationResult Validate(string token);
}
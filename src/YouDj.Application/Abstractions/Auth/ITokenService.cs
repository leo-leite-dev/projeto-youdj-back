using YouDj.Domain.Features.Common.ValueObjects;
using YouDj.Application.Features.Auth;

namespace YouDj.Application.Abstractions.Auth;

public interface IJwtTokenService
{
    Task<TokenResult> IssueAsync(Guid userId, Username username, IReadOnlyCollection<string> roles, IReadOnlyDictionary<string, string>? claims, CancellationToken ct);
    AuthTokenValidationResult Validate(string token);
}
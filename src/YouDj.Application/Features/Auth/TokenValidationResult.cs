namespace YouDj.Application.Features.Auth;

public sealed record AuthTokenValidationResult(
    bool IsValid,
    Guid? UserId,
    string? Username,
    IReadOnlyCollection<string> Roles,
    IReadOnlyDictionary<string, string> Claims
);
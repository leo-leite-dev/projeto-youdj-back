namespace YouDj.Application.Features.Auth;

public sealed record TokenResult(
    string AccessToken,
    DateTime ExpiresAtUtc,
    Guid UserId,
    string Username
);
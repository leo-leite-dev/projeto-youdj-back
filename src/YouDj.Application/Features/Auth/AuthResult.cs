namespace YouDj.Application.Features.Auth;

public sealed record AuthResult(
    string AccessToken,
    DateTime ExpiresAtUtc,
    Guid DjId,
    string Username
);
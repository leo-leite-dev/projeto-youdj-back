namespace YouDj.Application.Features.Auth.Guest;

public sealed record GuestTokenResult(
    string AccessToken,
    DateTime ExpiresAtUtc
);
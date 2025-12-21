namespace YouDj.Application.Features.Auth.Session;

public sealed record GuestSessionResult
{
    public required string DisplayName { get; init; }
    public required Guid DjId { get; init; }

    public required string AccessToken { get; init; }
    public required DateTime ExpiresAtUtc { get; init; }
}
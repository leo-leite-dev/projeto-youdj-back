namespace YouDj.Application.Features.Auth.Login.Guest;

public sealed record GuestLoginResult
{
    public required Guid GuestId { get; init; }
    public required string DisplayName { get; init; }
    public required int Credits { get; init; }

    public required string AccessToken { get; init; }
    public required DateTime ExpiresAtUtc { get; init; }
}

namespace YouDj.Api.Contracts.Auth;

public sealed record LoginResponse
{
    public Guid DjId { get; init; }
    public DateTime ExpiresAtUtc { get; init; }
    public bool IsDj { get; init; }
}
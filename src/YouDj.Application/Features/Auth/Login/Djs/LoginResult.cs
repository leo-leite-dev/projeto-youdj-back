namespace YouDj.Application.Features.Auth.Login.Dj;

public sealed record LoginResult
{
    public Guid DjId { get; init; }
    public string AccessToken { get; init; } = string.Empty;
    public DateTime ExpiresAtUtc { get; init; }
    public bool IsDj { get; init; }
}
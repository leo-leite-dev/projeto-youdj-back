namespace YouDj.Application.Features.Auth.Login;

public sealed record LoginResult
{
    public string AccessToken { get; init; } = string.Empty;
    public DateTime ExpiresAtUtc { get; init; }
    public bool IsDj { get; init; }
}
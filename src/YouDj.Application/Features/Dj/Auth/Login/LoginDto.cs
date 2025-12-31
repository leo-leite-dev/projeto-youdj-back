namespace YouDj.Application.Features.Dj.Auth.Login;

public sealed record LoginDto
{
    public Guid DjId { get; init; }
    public Guid PlaylistId { get; init; }
    public string AccessToken { get; init; } = string.Empty;
    public DateTime ExpiresAtUtc { get; init; }
    public bool IsDj { get; init; }
}
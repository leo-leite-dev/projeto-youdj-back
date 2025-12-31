namespace YouDj.Application.Features.Dj.Auth.Session;

public sealed class PlaylistSessionResult
{
    public required string AccessToken { get; init; }
    public required DateTime ExpiresAtUtc { get; init; }

    public required Guid DjId { get; init; }
    public required Guid PlaylistId { get; init; }
    public required string DisplayName { get; init; }
}
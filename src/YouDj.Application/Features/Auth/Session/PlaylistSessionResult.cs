namespace YouDj.Application.Features.Auth.PlaylistSession;

public sealed class PlaylistSessionResult
{
    public required string AccessToken { get; init; }
    public required DateTime ExpiresAtUtc { get; init; }

    public required Guid DjId { get; init; }
    public required Guid PlaylistId { get; init; }
    public required string DisplayName { get; init; }
}
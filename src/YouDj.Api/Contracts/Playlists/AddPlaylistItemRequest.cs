namespace YouDj.Api.Contracts.Playlists;

public sealed class AddPlaylistItemRequest
{
    public required Guid PlaylistId { get; init; }
    public Guid? FolderId { get; init; }

    public required string ExternalId { get; init; }
    public required string Title { get; init; }
    public required string ThumbnailUrl { get; init; }
    public required string Source { get; init; }
}
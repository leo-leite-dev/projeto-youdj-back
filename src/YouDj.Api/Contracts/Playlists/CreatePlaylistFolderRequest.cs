namespace YouDj.Api.Contracts.Playlists;

public sealed class CreatePlaylistFolderRequest
{
    public required Guid PlaylistId { get; init; }
    public required string Name { get; init; }
}

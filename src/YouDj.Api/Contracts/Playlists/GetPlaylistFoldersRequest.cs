namespace YouDj.Api.Contracts.Playlists;

public sealed record GetPlaylistFoldersRequest
{
    public required Guid PlaylistId { get; init; }
}
using YouDj.Domain.Features.Dj.Entities.Playlists;

namespace YouDj.Application.Abstractions.Repositories.Dj.Playlists;


public interface IPlaylistItemRepository
{
    Task AddAsync(PlaylistItem item, CancellationToken ct = default);
    void Remove(PlaylistItem item);

    Task<bool> ExistsAsync(Guid playlistId, string externalId, CancellationToken ct = default);
    Task<int> GetLastPositionAsync(Guid playlistId, Guid? folderId, CancellationToken ct = default);
    Task<PlaylistItem?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IReadOnlyList<PlaylistItem>> GetByFolderAsync(Guid playlistId, Guid? folderId, CancellationToken ct = default);
    Task<List<PlaylistItem>> GetByFolderWithTrackingAsync(Guid playlistId, Guid? folderId, CancellationToken ct = default);
}
using YouDj.Domain.Features.Dj.Entities.Playlists;

namespace YouDj.Application.Abstractions.Repositories.Dj.Playlists;

public interface IPlaylistFolderRepository
{
    Task AddAsync(PlaylistFolder folder, CancellationToken ct = default);
    void Remove(PlaylistFolder folder);

    Task<PlaylistFolder?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IReadOnlyList<PlaylistFolder>> GetByPlaylistAsync(Guid playlistId, CancellationToken ct = default);
    Task<int> GetLastPositionAsync(Guid playlistId, CancellationToken ct = default);
    Task<List<PlaylistFolder>> GetByPlaylistWithTrackingAsync(Guid playlistId, CancellationToken ct = default);
}
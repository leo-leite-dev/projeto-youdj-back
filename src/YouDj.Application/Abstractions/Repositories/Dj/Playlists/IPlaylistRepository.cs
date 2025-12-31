using YouDj.Domain.Features.Dj.Entities.Playlists;

namespace YouDj.Application.Abstractions.Repositories.Dj.Playlists;

public interface IPlaylistRepository
{
    Task AddAsync(Playlist playlist, CancellationToken ct = default);
    Task<Playlist?> GetByPublicTokenAsync(string publicToken, CancellationToken ct = default);
    Task<Playlist?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task UpdateAsync(Playlist playlist, CancellationToken ct = default);
}
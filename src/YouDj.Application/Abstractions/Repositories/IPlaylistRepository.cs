using YouDj.Domain.Features.Playlists;

namespace YouDj.Application.Abstractions.Repositories;

public interface IPlaylistRepository
{
    Task AddAsync(Playlist playlist, CancellationToken ct = default);
    Task<Playlist?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task UpdateAsync(Playlist playlist, CancellationToken ct = default);
}
using YouDj.Domain.Player;

namespace YouDj.Application.Abstractions.Repositories;

public interface INowPlayingRepository
{
    Task<NowPlaying?> GetByDjIdAsync(Guid djId, CancellationToken ct);
    Task AddAsync(NowPlaying nowPlaying, CancellationToken ct);
    void Remove(NowPlaying nowPlaying);
}
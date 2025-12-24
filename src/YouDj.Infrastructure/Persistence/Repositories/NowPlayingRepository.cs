using Microsoft.EntityFrameworkCore;
using YouDj.Application.Abstractions.Repositories;
using YouDj.Domain.Player;

namespace YouDj.Infrastructure.Persistence.Repositories;

public sealed class NowPlayingRepository : INowPlayingRepository
{
    private readonly YouDjDbContext _context;

    public NowPlayingRepository(YouDjDbContext context)
    {
        _context = context;
    }

    public async Task<NowPlaying?> GetByDjIdAsync(Guid djId, CancellationToken ct)
    {
        return await _context.Set<NowPlaying>()
            .FirstOrDefaultAsync(x => x.DjId == djId, ct);
    }

    public async Task AddAsync(NowPlaying nowPlaying, CancellationToken ct)
    {
        await _context.Set<NowPlaying>()
            .AddAsync(nowPlaying, ct);
    }

    public void Remove(NowPlaying nowPlaying)
    {
        _context.Set<NowPlaying>()
            .Remove(nowPlaying);
    }
}
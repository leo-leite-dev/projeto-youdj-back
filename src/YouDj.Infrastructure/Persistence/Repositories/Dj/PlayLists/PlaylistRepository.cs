using Microsoft.EntityFrameworkCore;
using YouDj.Application.Abstractions.Repositories.Dj.Playlists;
using YouDj.Domain.Features.Dj.Entities.Playlists;

namespace YouDj.Infrastructure.Persistence.Repositories.Playlists;

public sealed class PlaylistRepository : IPlaylistRepository
{
    private readonly YouDjDbContext _dbContext;

    public PlaylistRepository(YouDjDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Playlist playlist, CancellationToken ct = default)
    {
        await _dbContext.Playlists.AddAsync(playlist, ct);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task<Playlist?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _dbContext.Playlists
            .FirstOrDefaultAsync(p => p.Id == id, ct);
    }

    public async Task<Playlist?> GetByPublicTokenAsync(
        string publicToken, CancellationToken ct = default)
    {
        return await _dbContext.Playlists
            .FirstOrDefaultAsync(p => p.PublicToken == publicToken, ct);
    }

    public async Task UpdateAsync(Playlist playlist, CancellationToken ct = default)
    {
        _dbContext.Playlists.Update(playlist);
        await _dbContext.SaveChangesAsync(ct);
    }
}
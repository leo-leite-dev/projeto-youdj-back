using Microsoft.EntityFrameworkCore;
using YouDj.Application.Abstractions.Repositories.Dj.Playlists;
using YouDj.Domain.Features.Dj.Entities.Playlists;

namespace YouDj.Infrastructure.Persistence.Repositories.Dj.Playlists;

public sealed class PlaylistFolderRepository : IPlaylistFolderRepository
{
    private readonly YouDjDbContext _dbContext;

    public PlaylistFolderRepository(YouDjDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(
        PlaylistFolder folder, CancellationToken ct = default)
    {
        await _dbContext.PlaylistFolders.AddAsync(folder, ct);
    }

    public void Remove(PlaylistFolder folder)
    {
        _dbContext.PlaylistFolders.Remove(folder);
    }

    public async Task<PlaylistFolder?> GetByIdAsync(
        Guid id, CancellationToken ct = default)
    {
        return await _dbContext.PlaylistFolders
            .FirstOrDefaultAsync(x => x.Id == id, ct);
    }

    public async Task<IReadOnlyList<PlaylistFolder>> GetByPlaylistAsync(
        Guid playlistId, CancellationToken ct = default)
    {
        return await _dbContext.PlaylistFolders
            .Where(x => x.PlaylistId == playlistId)
            .OrderBy(x => x.Position)
            .AsNoTracking()
            .ToListAsync(ct);
    }

    public async Task<int> GetLastPositionAsync(
        Guid playlistId, CancellationToken ct = default)
    {
        return await _dbContext.PlaylistFolders
            .Where(x => x.PlaylistId == playlistId)
            .Select(x => (int?)x.Position)
            .MaxAsync(ct)
            ?? -1;
    }

    public async Task<List<PlaylistFolder>> GetByPlaylistWithTrackingAsync(
        Guid playlistId, CancellationToken ct = default)
    {
        return await _dbContext.PlaylistFolders
            .Where(x => x.PlaylistId == playlistId)
            .OrderBy(x => x.Position)
            .ToListAsync(ct);
    }
}
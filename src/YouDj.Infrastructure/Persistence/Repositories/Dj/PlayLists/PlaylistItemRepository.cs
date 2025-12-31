using Microsoft.EntityFrameworkCore;
using YouDj.Application.Abstractions.Repositories.Dj.Playlists;
using YouDj.Domain.Features.Dj.Entities.Playlists;

namespace YouDj.Infrastructure.Persistence.Repositories.Playlists;

public sealed class PlaylistItemRepository : IPlaylistItemRepository
{
    private readonly YouDjDbContext _dbContext;

    public PlaylistItemRepository(YouDjDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(PlaylistItem item, CancellationToken ct = default)
    {
        await _dbContext.PlaylistItems.AddAsync(item, ct);
    }

    public void Remove(PlaylistItem item)
    {
        _dbContext.PlaylistItems.Remove(item);
    }


    public async Task<bool> ExistsAsync(
        Guid playlistId, string externalId, CancellationToken ct = default)
    {
        return await _dbContext.PlaylistItems
            .AnyAsync(x =>
                x.PlaylistId == playlistId &&
                x.Track.ExternalId == externalId,
                ct);
    }

    public async Task<int> GetLastPositionAsync(
        Guid playlistId, Guid? folderId, CancellationToken ct = default)
    {
        return await _dbContext.PlaylistItems
            .Where(x =>
                x.PlaylistId == playlistId &&
                x.FolderId == folderId)
            .Select(x => (int?)x.Position)
            .MaxAsync(ct)
            ?? -1;
    }

    public async Task<PlaylistItem?> GetByIdAsync(
        Guid id, CancellationToken ct = default)
    {
        return await _dbContext.PlaylistItems
            .FirstOrDefaultAsync(x => x.Id == id, ct);
    }

    public async Task<IReadOnlyList<PlaylistItem>> GetByFolderAsync(
        Guid playlistId, Guid? folderId, CancellationToken ct = default)
    {
        return await _dbContext.PlaylistItems
            .Where(x =>
                x.PlaylistId == playlistId &&
                x.FolderId == folderId)
            .OrderBy(x => x.Position)
            .AsNoTracking()
            .ToListAsync(ct);
    }

    public async Task<List<PlaylistItem>> GetByFolderWithTrackingAsync(
        Guid playlistId, Guid? folderId, CancellationToken ct = default)
    {
        return await _dbContext.PlaylistItems
            .Where(x =>
                x.PlaylistId == playlistId &&
                x.FolderId == folderId)
            .OrderBy(x => x.Position)
            .ToListAsync(ct);
    }
}
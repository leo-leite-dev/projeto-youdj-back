using Microsoft.EntityFrameworkCore;
using YouDj.Application.Abstractions.Repositories;
using YouDj.Domain.Queue;

namespace YouDj.Infrastructure.Persistence.Repositories;

public sealed class QueueRepository : IQueueRepository
{
    private readonly YouDjDbContext _context;

    public QueueRepository(YouDjDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(QueueItem item, CancellationToken ct)
    {
        await _context.QueueItems.AddAsync(item, ct);
    }

    public async Task<IReadOnlyList<QueueItem>> GetByDjAsync(
        Guid djId, CancellationToken ct)
    {
        return await _context.QueueItems
            .Where(x =>
                x.DjId == djId &&
                x.Status == QueueStatus.Queued)
            .OrderBy(x => x.Position)
            .AsNoTracking()
            .ToListAsync(ct);
    }

    public async Task<List<QueueItem>> GetByDjWithTrackingAsync(
        Guid djId, CancellationToken ct)
    {
        return await _context.QueueItems
            .Where(x =>
                x.DjId == djId &&
                x.Status == QueueStatus.Queued)
            .OrderBy(x => x.Position)
            .ToListAsync(ct);
    }

    public async Task<int> GetLastPositionAsync(Guid djId, CancellationToken ct)
    {
        return await _context.QueueItems
            .Where(x =>
                x.DjId == djId &&
                x.Status == QueueStatus.Queued)
            .Select(x => (int?)x.Position)
            .MaxAsync(ct)
            ?? -1;
    }

    public async Task<QueueItem?> GetFirstAsync(Guid djId, CancellationToken ct)
    {
        return await _context.QueueItems
            .Where(x =>
                x.DjId == djId &&
                x.Status == QueueStatus.Queued)
            .OrderBy(x => x.Position)
            .FirstOrDefaultAsync(ct);
    }

    public void Remove(QueueItem item)
    {
        _context.QueueItems.Remove(item);
    }
}
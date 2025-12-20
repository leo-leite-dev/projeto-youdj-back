using YouDj.Application.Abstractions.Repositories;
using YouDj.Domain.Queue;
using YouDj.Infrastructure.Persistence;

namespace YouDj.Infrastructure.Data.Repositories;

public sealed class QueueRepository : IQueueRepository
{
    private readonly YouDjDbContext _context;

    public QueueRepository(YouDjDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(QueueItem item, CancellationToken ct)
    {
        _context.QueueItems.Add(item);
        await _context.SaveChangesAsync(ct);
    }
}
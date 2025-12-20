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
        await _context.SaveChangesAsync(ct);
    }
}
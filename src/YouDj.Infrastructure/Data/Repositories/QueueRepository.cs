using YouDj.Application.Abstractions.Repositories;
using YouDj.Domain.Queue;

namespace YouDj.Infrastructure.Data.Repositories;

public sealed class QueueRepository : IQueueRepository
{
    private readonly AppDbContext _context;

    public QueueRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(QueueItem item, CancellationToken ct)
    {
        _context.QueueItems.Add(item);
        await _context.SaveChangesAsync(ct);
    }
}
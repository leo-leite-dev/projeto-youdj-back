using YouDj.Domain.Queue;

namespace YouDj.Application.Abstractions.Repositories;

public interface IQueueRepository
{
    Task AddAsync(QueueItem item, CancellationToken ct);
    // Task<IReadOnlyList<QueueItem>> GetByDjAsync(Guid djId, CancellationToken ct);
    // Task<QueueItem?> GetNextAsync(Guid djId, CancellationToken ct);
    // Task UpdateAsync(QueueItem item, CancellationToken ct);
    // Task RemoveAsync(Guid queueItemId, CancellationToken ct);
}
using YouDj.Domain.Queue;

namespace YouDj.Application.Abstractions.Repositories;

public interface IQueueRepository
{
    Task AddAsync(QueueItem item, CancellationToken ct);
    void Remove(QueueItem item);

    Task<IReadOnlyList<QueueItem>> GetByDjAsync(Guid djId, CancellationToken ct);
    Task<List<QueueItem>> GetByDjWithTrackingAsync(Guid djId, CancellationToken ct);
    Task<QueueItem?> GetFirstAsync(Guid djId, CancellationToken ct);
    Task<int> GetLastPositionAsync(Guid djId, CancellationToken ct);
}
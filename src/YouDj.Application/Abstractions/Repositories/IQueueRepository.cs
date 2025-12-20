using YouDj.Domain.Queue;

namespace YouDj.Application.Abstractions.Repositories;

public interface IQueueRepository
{
    Task AddAsync(QueueItem item, CancellationToken ct);
}
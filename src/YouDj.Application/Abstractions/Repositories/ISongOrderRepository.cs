using YouDj.Domain.SongOrders;

namespace YouDj.Application.Abstractions.Repositories;

public interface ISongOrderRepository
{
    Task<DjSongOrder?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<IReadOnlyList<DjSongOrder>> GetPendingByDjAsync(Guid djId, CancellationToken ct);
    Task AddAsync(DjSongOrder order, CancellationToken ct);
}
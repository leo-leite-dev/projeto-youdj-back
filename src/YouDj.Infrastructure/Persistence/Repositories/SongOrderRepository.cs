using Microsoft.EntityFrameworkCore;
using YouDj.Application.Abstractions.Repositories;
using YouDj.Domain.Features.Common.Enums;
using YouDj.Domain.SongOrders;
using YouDj.Infrastructure.Persistence;

namespace YouDj.Infrastructure.Persistences.Repositories;

public sealed class SongOrderRepository : ISongOrderRepository
{
    private readonly YouDjDbContext _context;

    public SongOrderRepository(YouDjDbContext context)
    {
        _context = context;
    }

    public async Task<DjSongOrder?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await _context.DjSongOrders
            .FirstOrDefaultAsync(o => o.Id == id, ct);
    }

    public async Task<IReadOnlyList<DjSongOrder>> GetPendingByDjAsync(
        Guid djId, CancellationToken ct)
    {
        return await _context.DjSongOrders
            .Where(o =>
                o.DjId == djId &&
                o.Status == DjSongOrderStatus.Pending)
            .OrderBy(o => o.CreatedAtUtc)
            .ToListAsync(ct);
    }

    public async Task AddAsync(DjSongOrder order, CancellationToken ct)
    {
        await _context.DjSongOrders.AddAsync(order, ct);
    }
}
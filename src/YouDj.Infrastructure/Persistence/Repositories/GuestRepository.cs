using Microsoft.EntityFrameworkCore;
using YouDj.Application.Features.Repositories;
using YouDj.Domain.Features.Guests;
using YouDj.Infrastructure.Persistence;

public sealed class GuestRepository : IGuestRepository
{
    private readonly YouDjDbContext _dbContext;

    public GuestRepository(YouDjDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task AddAsync(Guest guest, CancellationToken ct)
        => _dbContext.Guests.AddAsync(guest, ct).AsTask();

    public Task<Guest?> GetByIdAsync(Guid id, CancellationToken ct)
        => _dbContext.Guests.FindAsync(new object[] { id }, ct).AsTask();

    public async Task<Guest?> GetByPhoneAsync(string phone, CancellationToken ct)
        => await _dbContext.Guests
            .FirstOrDefaultAsync(g => g.Phone == phone, ct);

    public Task UpdateAsync(Guest guest, CancellationToken ct)
        => Task.CompletedTask;
}
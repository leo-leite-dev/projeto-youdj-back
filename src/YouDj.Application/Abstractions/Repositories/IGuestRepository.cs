using YouDj.Domain.Features.Guests;

namespace YouDj.Application.Features.Repositories;

public interface IGuestRepository
{
    Task AddAsync(Guest guest, CancellationToken ct);
    Task<Guest?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<Guest?> GetByPhoneAsync(string phone, CancellationToken ct);
    Task UpdateAsync(Guest guest, CancellationToken ct);
}

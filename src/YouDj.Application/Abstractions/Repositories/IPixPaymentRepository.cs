using YouDj.Domain.Features.Payments;

namespace YouDj.Application.Features.Repositories;

public interface IPixPaymentRepository
{
    Task AddAsync(PixPayment payment, CancellationToken ct);
    Task<PixPayment?> GetByIdAsync(Guid id, CancellationToken ct);
}
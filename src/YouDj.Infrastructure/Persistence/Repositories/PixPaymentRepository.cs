using Microsoft.EntityFrameworkCore;
using YouDj.Application.Features.Repositories;
using YouDj.Domain.Features.Payments;
using YouDj.Infrastructure.Persistence;

public sealed class PixPaymentRepository : IPixPaymentRepository
{
    private readonly YouDjDbContext _dbContext;

    public PixPaymentRepository(YouDjDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task AddAsync(PixPayment payment, CancellationToken ct)
        => _dbContext.PixPayments.AddAsync(payment, ct).AsTask();

    public Task<PixPayment?> GetByIdAsync(Guid id, CancellationToken ct)
        => _dbContext.PixPayments
            .FirstOrDefaultAsync(p => p.Id == id, ct);
}
using YouDj.Application.Abstractions.Persistences;

namespace YouDj.Infrastructure.Persistence;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly YouDjDbContext _dbContext;

    public UnitOfWork(YouDjDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task CommitAsync(CancellationToken ct)
        => _dbContext.SaveChangesAsync(ct);
}
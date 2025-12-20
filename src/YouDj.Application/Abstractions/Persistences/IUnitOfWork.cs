namespace YouDj.Application.Abstractions.Persistences;

public interface IUnitOfWork
{
    Task CommitAsync(CancellationToken ct);
}
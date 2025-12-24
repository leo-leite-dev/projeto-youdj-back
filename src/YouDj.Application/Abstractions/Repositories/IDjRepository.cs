using YouDj.Domain.Features.Common.ValueObjects;
using YouDj.Domain.Features.Users.Entities;
using YouDj.Domain.Features.Users.ValueObjects;

namespace YouDj.Application.Abstractions.Repositories;

public interface IDjRepository
{
    Task<Dj?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<Dj?> GetByEmailAsync(Email email, CancellationToken ct = default);
    Task<Dj?> GetByUsernameAsync(Username Username, CancellationToken ct = default);

    Task<bool> EmailExistsAsync(Email email, CancellationToken ct = default);
    Task<bool> UsernameExistsAsync(Username Username, CancellationToken ct = default);

    Task AddAsync(Dj Dj, CancellationToken ct = default);
    Task UpdateAsync(Dj Dj, CancellationToken ct = default);
}
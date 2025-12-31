using YouDj.Domain.Features.Common.ValueObjects;
using YouDj.Domain.Features.Dj.Entities.User;
using YouDj.Domain.Features.Users.ValueObjects;

namespace YouDj.Application.Abstractions.Repositories;

public interface IDjRepository
{
    Task<UserDj?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<UserDj?> GetByEmailAsync(Email email, CancellationToken ct = default);
    Task<UserDj?> GetByUsernameAsync(Username Username, CancellationToken ct = default);

    Task<bool> EmailExistsAsync(Email email, CancellationToken ct = default);
    Task<bool> UsernameExistsAsync(Username Username, CancellationToken ct = default);

    Task AddAsync(UserDj UserDj, CancellationToken ct = default);
    Task UpdateAsync(UserDj UserDj, CancellationToken ct = default);
}
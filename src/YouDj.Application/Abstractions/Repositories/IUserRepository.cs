using YouDj.Domain.Features.Users.Entities;
using YouDj.Domain.Features.Common.ValueObjects;
using YouDj.Domain.Features.Uasers.ValueObjects;

namespace YouDj.Application.Abstractions.Repositories;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<User?> GetByEmailAsync(Email email, CancellationToken ct = default);
    Task<User?> GetByUsernameAsync(Username username, CancellationToken ct = default);
    Task<User?> GetByLoginAsync(string login, CancellationToken ct = default);

    Task<bool> EmailExistsAsync(Email email, CancellationToken ct = default);
    Task<bool> UsernameExistsAsync(Username username, CancellationToken ct = default);

    Task AddAsync(User user, CancellationToken ct = default);
    Task UpdateAsync(User user, CancellationToken ct = default);
}
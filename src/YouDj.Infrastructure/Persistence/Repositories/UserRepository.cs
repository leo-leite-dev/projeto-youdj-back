using Microsoft.EntityFrameworkCore;
using YouDj.Domain.Features.Users.Entities;
using YouDj.Domain.Features.Common.ValueObjects;
using YouDj.Application.Abstractions.Repositories;
using YouDj.Domain.Features.Uasers.ValueObjects;

namespace YouDj.Infrastructure.Persistence.Repositories;

public sealed class UserRepository : IUserRepository
{
    private readonly YouDjDbContext _context;

    public UserRepository(YouDjDbContext context)
    {
        _context = context;
    }

    public Task<User?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id, ct);

    public Task<User?> GetByEmailAsync(Email email, CancellationToken ct = default)
        => _context.Users
            .FirstOrDefaultAsync(u => u.Email == email, ct);

    public Task<User?> GetByUsernameAsync(Username username, CancellationToken ct = default)
        => _context.Users
            .FirstOrDefaultAsync(u => u.Username == username, ct);

    public Task<User?> GetByLoginAsync(string login, CancellationToken ct = default)
        => _context.Users.FirstOrDefaultAsync(
            u => u.Email.Value == login || u.Username.Value == login,
            ct
        );

    public Task<bool> EmailExistsAsync(Email email, CancellationToken ct = default)
        => _context.Users.AnyAsync(u => u.Email == email, ct);

    public Task<bool> UsernameExistsAsync(Username username, CancellationToken ct = default)
        => _context.Users.AnyAsync(u => u.Username == username, ct);

    public async Task AddAsync(User user, CancellationToken ct = default)
    {
        await _context.Users.AddAsync(user, ct);
        await _context.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(User user, CancellationToken ct = default)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync(ct);
    }
}
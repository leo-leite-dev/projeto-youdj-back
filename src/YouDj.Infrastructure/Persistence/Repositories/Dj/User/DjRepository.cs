using Microsoft.EntityFrameworkCore;
using YouDj.Domain.Features.Common.ValueObjects;
using YouDj.Application.Abstractions.Repositories;
using YouDj.Domain.Features.Users.ValueObjects;
using YouDj.Domain.Features.Dj.Entities.User;

namespace YouDj.Infrastructure.Persistence.Repositories.Dj.User;

public sealed class DjRepository : IDjRepository
{
    private readonly YouDjDbContext _context;

    public DjRepository(YouDjDbContext context)
    {
        _context = context;
    }

    public Task<UserDj?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => _context.Djs
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id, ct);

    public Task<UserDj?> GetByEmailAsync(Email email, CancellationToken ct = default)
        => _context.Djs
            .FirstOrDefaultAsync(u => u.Email == email, ct);

    public Task<UserDj?> GetByUsernameAsync(Username Username, CancellationToken ct = default)
        => _context.Djs
            .FirstOrDefaultAsync(u => u.Username == Username, ct);

    public Task<bool> EmailExistsAsync(Email email, CancellationToken ct = default)
        => _context.Djs.AnyAsync(u => u.Email == email, ct);

    public Task<bool> UsernameExistsAsync(Username Username, CancellationToken ct = default)
        => _context.Djs.AnyAsync(u => u.Username == Username, ct);

    public async Task AddAsync(UserDj UserDj, CancellationToken ct = default)
    {
        await _context.Djs.AddAsync(UserDj, ct);
        await _context.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(UserDj UserDj, CancellationToken ct = default)
    {
        _context.Djs.Update(UserDj);
        await _context.SaveChangesAsync(ct);
    }
}
using Microsoft.EntityFrameworkCore;
using YouDj.Domain.Features.Common.ValueObjects;
using YouDj.Application.Abstractions.Repositories;
using YouDj.Domain.Features.Users.Entities;
using YouDj.Domain.Features.Users.ValueObjects;

namespace YouDj.Infrastructure.Persistence.Repositories;

public sealed class DjRepository : IDjRepository
{
    private readonly YouDjDbContext _context;

    public DjRepository(YouDjDbContext context)
    {
        _context = context;
    }

    public Task<Dj?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => _context.Djs
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id, ct);

    public Task<Dj?> GetByEmailAsync(Email email, CancellationToken ct = default)
        => _context.Djs
            .FirstOrDefaultAsync(u => u.Email == email, ct);

    public Task<Dj?> GetByUsernameAsync(Username Username, CancellationToken ct = default)
        => _context.Djs
            .FirstOrDefaultAsync(u => u.Username == Username, ct);

    public Task<bool> EmailExistsAsync(Email email, CancellationToken ct = default)
        => _context.Djs.AnyAsync(u => u.Email == email, ct);

    public Task<bool> UsernameExistsAsync(Username Username, CancellationToken ct = default)
        => _context.Djs.AnyAsync(u => u.Username == Username, ct);

    public async Task AddAsync(Dj Dj, CancellationToken ct = default)
    {
        await _context.Djs.AddAsync(Dj, ct);
        await _context.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(Dj Dj, CancellationToken ct = default)
    {
        _context.Djs.Update(Dj);
        await _context.SaveChangesAsync(ct);
    }
}
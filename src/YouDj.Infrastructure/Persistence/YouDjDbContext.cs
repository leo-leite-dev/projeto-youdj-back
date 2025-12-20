using Microsoft.EntityFrameworkCore;
using YouDj.Domain.Features.Users.Entities;
using YouDj.Domain.Queue;

namespace YouDj.Infrastructure.Persistence;

public sealed class YouDjDbContext : DbContext
{
    public YouDjDbContext(DbContextOptions<YouDjDbContext> options)
        : base(options) { }

    public DbSet<QueueItem> QueueItems => Set<QueueItem>();
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(YouDjDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}

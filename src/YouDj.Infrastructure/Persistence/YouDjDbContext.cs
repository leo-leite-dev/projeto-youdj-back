using Microsoft.EntityFrameworkCore;
using YouDj.Domain.Features.Guests;
using YouDj.Domain.Features.Payments;
using YouDj.Domain.Features.Playlists;
using YouDj.Domain.Features.Users.Entities;
using YouDj.Domain.Queue;
using YouDj.Domain.SongOrders;

namespace YouDj.Infrastructure.Persistence;

public sealed class YouDjDbContext : DbContext
{
    public YouDjDbContext(DbContextOptions<YouDjDbContext> options)
        : base(options) { }

    public DbSet<QueueItem> QueueItems => Set<QueueItem>();
    public DbSet<Dj> Djs => Set<Dj>();
    public DbSet<Playlist> Playlists => Set<Playlist>();
    public DbSet<Guest> Guests => Set<Guest>();
    public DbSet<PixPayment> PixPayments => Set<PixPayment>();
    public DbSet<DjSongOrder> DjSongOrders => Set<DjSongOrder>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(YouDjDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}

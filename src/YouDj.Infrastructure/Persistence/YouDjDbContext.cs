using Microsoft.EntityFrameworkCore;
using YouDj.Domain.Features.Dj.Entities.Playlists;
using YouDj.Domain.Features.Dj.Entities.User;
using YouDj.Domain.Features.Guests;
using YouDj.Domain.Features.Payments;
using YouDj.Domain.Queue;
using YouDj.Domain.SongOrders;

namespace YouDj.Infrastructure.Persistence;

public sealed class YouDjDbContext : DbContext
{
    public YouDjDbContext(DbContextOptions<YouDjDbContext> options)
        : base(options) { }

    public DbSet<QueueItem> QueueItems => Set<QueueItem>();
    public DbSet<UserDj> Djs => Set<UserDj>();
    public DbSet<Playlist> Playlists => Set<Playlist>();
    public DbSet<PlaylistItem> PlaylistItems => Set<PlaylistItem>();
    public DbSet<PlaylistFolder> PlaylistFolders => Set<PlaylistFolder>();
    public DbSet<Guest> Guests => Set<Guest>();
    public DbSet<PixPayment> PixPayments => Set<PixPayment>();
    public DbSet<DjSongOrder> DjSongOrders => Set<DjSongOrder>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(YouDjDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}

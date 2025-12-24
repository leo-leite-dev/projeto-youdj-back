using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YouDj.Domain.SongOrders;

namespace YouDj.Infrastructure.Persistence.Configurations;

public sealed class DjSongOrderConfiguration
    : IEntityTypeConfiguration<DjSongOrder>
{
    public void Configure(EntityTypeBuilder<DjSongOrder> builder)
    {
        builder.ToTable("dj_song_orders");

        builder.HasKey(o => o.Id);

        builder.Property(o => o.Id)
            .ValueGeneratedNever();

        builder.Property(o => o.DjId)
            .HasColumnName("dj_id")
            .IsRequired();

        builder.Property(o => o.GuestId)
            .HasColumnName("guest_id")
            .IsRequired();

        builder.Property(o => o.PriceInCredits)
            .HasColumnName("price_in_credits")
            .IsRequired();

        builder.Property(o => o.ExternalId)
            .HasColumnName("external_id")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(o => o.Title)
            .HasColumnName("title")
            .HasMaxLength(300)
            .IsRequired();

        builder.Property(o => o.ThumbnailUrl)
            .HasColumnName("thumbnail_url")
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(o => o.Source)
            .HasColumnName("source")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(o => o.Duration)
            .HasColumnName("duration");

        builder.Property(o => o.Status)
            .HasColumnName("status")
            .HasConversion<int>()
            .IsRequired();

        builder.Property(o => o.CreatedAtUtc)
            .HasColumnName("created_at_utc")
            .IsRequired();

        builder.Property(o => o.UpdatedAtUtc)
            .HasColumnName("updated_at_utc");

        builder.HasIndex(o => o.DjId)
            .HasDatabaseName("ix_dj_song_orders_dj_id");

        builder.HasIndex(o => o.GuestId)
            .HasDatabaseName("ix_dj_song_orders_guest_id");

        builder.HasIndex(o => o.Status)
            .HasDatabaseName("ix_dj_song_orders_status");
    }
}
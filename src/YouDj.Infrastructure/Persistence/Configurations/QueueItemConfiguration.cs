using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YouDj.Domain.Queue;

namespace YouDj.Infrastructure.Persistence.Configurations;

public sealed class QueueItemConfiguration : IEntityTypeConfiguration<QueueItem>
{
    public void Configure(EntityTypeBuilder<QueueItem> builder)
    {
        builder.ToTable("queue_items");

        builder.HasKey(q => q.Id);

        builder.Property(q => q.Id)
            .ValueGeneratedNever();

        builder.Property(q => q.DjId)
            .HasColumnName("dj_id")
            .IsRequired();

        builder.Property(q => q.GuestId)
            .HasColumnName("guest_id")
            .IsRequired();

        builder.Property(q => q.PriceInCredits)
            .HasColumnName("price_in_credits")
            .IsRequired();

        builder.OwnsOne(q => q.Track, track =>
        {
            track.Property(t => t.ExternalId)
                .HasColumnName("external_id")
                .HasMaxLength(100)
                .IsRequired();

            track.Property(t => t.Title)
                .HasColumnName("title")
                .HasMaxLength(255)
                .IsRequired();

            track.Property(t => t.ThumbnailUrl)
                .HasColumnName("thumbnail_url")
                .HasMaxLength(500)
                .IsRequired();

            track.Property(t => t.Source)
                .HasColumnName("source")
                .HasMaxLength(50)
                .IsRequired();
        });

        builder.Property(q => q.Duration)
            .HasColumnName("duration");

        builder.Property(q => q.Status)
            .HasColumnName("status")
            .HasConversion<int>()
            .IsRequired();

        builder.Property(q => q.Position)
            .HasColumnName("position")
            .IsRequired();

        builder.Property(q => q.CreatedAtUtc)
            .HasColumnName("created_at_utc")
            .IsRequired();

        builder.Property(q => q.UpdatedAtUtc)
            .HasColumnName("updated_at_utc");

        builder.HasIndex(q => new { q.DjId, q.Status })
            .HasDatabaseName("ix_queue_items_dj_status");
    }
}
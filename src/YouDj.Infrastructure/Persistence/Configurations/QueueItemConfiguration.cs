using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YouDj.Domain.Queue;

namespace YouDj.Infrastructure.Data.Configurations;

public sealed class QueueItemConfiguration
    : IEntityTypeConfiguration<QueueItem>
{
    public void Configure(EntityTypeBuilder<QueueItem> builder)
    {
        builder.ToTable("queue_items");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.ExternalId)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(x => x.ThumbnailUrl)
            .HasMaxLength(500);

        builder.Property(x => x.Source)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Duration);

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.Position)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();
    }
}
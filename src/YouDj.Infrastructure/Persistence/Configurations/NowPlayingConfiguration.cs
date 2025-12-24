using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YouDj.Domain.Player;

namespace YouDj.Infrastructure.Persistence.Configurations;

public sealed class NowPlayingConfiguration : IEntityTypeConfiguration<NowPlaying>
{
    public void Configure(EntityTypeBuilder<NowPlaying> builder)
    {
        builder.ToTable("now_playing");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.DjId)
            .IsRequired();

        builder.HasIndex(x => x.DjId)
            .IsUnique();

        builder.Property(x => x.StartedAt)
            .IsRequired();

        builder.OwnsOne(x => x.Track, track =>
        {
            track.Property(t => t.ExternalId)
                .HasColumnName("ExternalId")
                .IsRequired()
                .HasMaxLength(200);

            track.Property(t => t.Title)
                .HasColumnName("Title")
                .IsRequired()
                .HasMaxLength(300);

            track.Property(t => t.ThumbnailUrl)
                .HasColumnName("ThumbnailUrl")
                .HasMaxLength(500);

            track.Property(t => t.Source)
                .HasColumnName("Source")
                .IsRequired()
                .HasMaxLength(50);
        });

        builder.Navigation(x => x.Track)
            .IsRequired();
    }
}
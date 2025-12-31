using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YouDj.Domain.Features.Dj.Entities.Playlists;

namespace YouDj.Infrastructure.Persistence.Configurations.Dj.Playlists;

public sealed class PlaylistItemConfiguration
    : IEntityTypeConfiguration<PlaylistItem>
{
    public void Configure(EntityTypeBuilder<PlaylistItem> builder)
    {
        builder.ToTable("playlist_items");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.PlaylistId)
            .IsRequired();

        builder.HasIndex(x => x.PlaylistId);

        builder.Property(x => x.FolderId);

        builder.Property(x => x.Position)
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
                .HasColumnName("Thumbnail")
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
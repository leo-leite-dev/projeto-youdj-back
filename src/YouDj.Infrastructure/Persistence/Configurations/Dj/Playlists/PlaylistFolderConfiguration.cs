using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YouDj.Domain.Features.Dj.Entities.Playlists;

namespace YouDj.Infrastructure.Persistence.Configurations.Dj.Playlists;

public sealed class PlaylistFolderConfiguration
    : IEntityTypeConfiguration<PlaylistFolder>
{
    public void Configure(EntityTypeBuilder<PlaylistFolder> builder)
    {
        builder.ToTable("playlist_folders");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.PlaylistId)
            .HasColumnName("playlist_id")
            .IsRequired();

        builder.Property(x => x.Name)
            .HasColumnName("name")
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.Position)
            .HasColumnName("position")
            .IsRequired();

        builder.Property(x => x.CreatedAtUtc)
            .HasColumnName("created_at_utc");

        builder.Property(x => x.UpdatedAtUtc)
            .HasColumnName("updated_at_utc");

        builder.HasIndex(x => new { x.PlaylistId, x.Position })
            .IsUnique(false);
    }
}
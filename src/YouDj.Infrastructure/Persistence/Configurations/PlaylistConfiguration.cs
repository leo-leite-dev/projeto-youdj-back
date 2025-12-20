using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YouDj.Domain.Features.Playlists;

namespace YouDj.Infrastructure.Persistence.Configurations;

public sealed class PlaylistConfiguration : IEntityTypeConfiguration<Playlist>
{
    public void Configure(EntityTypeBuilder<Playlist> builder)
    {
        builder.ToTable("playlists");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .ValueGeneratedNever();

        builder.Property(p => p.DjId)
            .HasColumnName("dj_id")
            .IsRequired();

        builder.HasIndex(p => p.DjId)
            .HasDatabaseName("ix_playlists_dj_id");

        builder.Property(p => p.Name)
            .HasColumnName("name")
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(p => p.PublicSlug)
            .HasColumnName("public_slug")
            .HasMaxLength(200)
            .IsRequired();

        builder.HasIndex(p => p.PublicSlug)
            .IsUnique()
            .HasDatabaseName("ux_playlists_public_slug");

        builder.Property(p => p.PublicToken)
            .HasColumnName("public_token")
            .HasMaxLength(32)
            .IsRequired();

        builder.Property(p => p.IsActive)
            .HasColumnName("is_active")
            .HasDefaultValue(true)
            .IsRequired();

        builder.Property(p => p.CreatedAtUtc)
            .HasColumnName("created_at_utc")
            .IsRequired();

        builder.Property(p => p.UpdatedAtUtc)
            .HasColumnName("updated_at_utc");
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YouDj.Domain.Features.Common.ValueObjects;
using YouDj.Domain.Features.Dj.Entities.User;
using YouDj.Domain.Features.Users.ValueObjects;

namespace YouDj.Infrastructure.Persistence.Configurations.Dj.User;

public sealed class UserConfiguration : IEntityTypeConfiguration<UserDj>
{
    public void Configure(EntityTypeBuilder<UserDj> builder)
    {
        builder.ToTable("users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .ValueGeneratedNever();

        builder.Property(u => u.IsActive)
            .HasColumnName("is_active")
            .HasDefaultValue(true)
            .IsRequired();

        builder.Property(u => u.CreatedAtUtc)
            .HasColumnName("created_at_utc")
            .IsRequired();

        builder.Property(u => u.UpdatedAtUtc)
            .HasColumnName("updated_at_utc");

        builder.Property(u => u.Username)
            .HasConversion(
                v => v.Value,
                v => Username.Parse(v))
            .HasColumnName("username")
            .HasColumnType("citext")
            .HasMaxLength(50)
            .IsRequired();

        builder.HasIndex(u => u.Username)
            .IsUnique()
            .HasDatabaseName("ux_users_username");

        builder.Property(u => u.Email)
            .HasConversion(
                v => v.Value,
                v => Email.Parse(v))
            .HasColumnName("email")
            .HasColumnType("citext")
            .HasMaxLength(256)
            .IsRequired();

        builder.HasIndex(u => u.Email)
            .IsUnique()
            .HasDatabaseName("ux_users_email");

        builder.Property(u => u.BirthDate)
            .HasConversion(
                v => v.Value,
                v => DateOfBirth.Parse(v))
            .HasColumnName("birth_date")
            .HasColumnType("date")
            .IsRequired();

        builder.Property(u => u.PasswordHash)
            .HasColumnName("password_hash")
            .IsRequired();

        builder.Property(u => u.PasswordResetToken)
            .HasColumnName("password_reset_token")
            .HasMaxLength(200);

        builder.Property(u => u.PasswordResetTokenExpiresAt)
            .HasColumnName("password_reset_token_expires_at");

        builder.Property(u => u.TokenVersion)
            .HasColumnName("token_version")
            .HasDefaultValue(0)
            .IsConcurrencyToken()
            .IsRequired();
    }
}
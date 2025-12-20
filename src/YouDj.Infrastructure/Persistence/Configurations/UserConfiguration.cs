using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using YouDj.Domain.Features.Common.ValueObjects;
using YouDj.Domain.Features.Users.Entities;
using YouDj.Domain.Features.Uasers.ValueObjects;

namespace YouDj.Infrastructure.Persistence.Configurations.Users;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        builder.HasKey(u => u.Id);

        var usernameConverter = new ValueConverter<Username, string>(
            v => v.Value,
            v => Username.Parse(v));

        var emailConverter = new ValueConverter<Email, string>(
            v => v.Value,
            v => Email.Parse(v));

        var birthDateConverter = new ValueConverter<DateOfBirth, DateOnly>(
            v => v.Value,
            v => DateOfBirth.Parse(v)
        );

        builder.Property(u => u.Id)
            .ValueGeneratedNever();

        builder.Property(u => u.IsActive)
            .HasColumnName("is_active")
            .HasDefaultValue(true)
            .IsRequired();

        builder.Property(u => u.CreatedAtUtc)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(u => u.UpdatedAtUtc)
            .HasColumnName("updated_at");

        builder.Property(u => u.Username)
            .HasConversion(usernameConverter)
            .HasColumnName("username")
            .HasColumnType("citext")
            .HasMaxLength(50)
            .IsRequired();

        builder.HasIndex(u => u.Username)
            .IsUnique()
            .HasDatabaseName("ux_users_username");

        builder.Property(u => u.Email)
            .HasConversion(emailConverter)
            .HasColumnName("email")
            .HasColumnType("citext")
            .HasMaxLength(256)
            .IsRequired();

        builder.HasIndex(u => u.Email)
            .IsUnique()
            .HasDatabaseName("ux_users_email");

        builder.Property(u => u.BirthDate)
            .HasConversion(birthDateConverter)
            .HasColumnName("birth_date")
            .HasColumnType("date")
            .IsRequired();

        builder.Property(u => u.PasswordHash)
            .HasColumnName("password_hash")
            .IsRequired();

        builder.Property(u => u.PasswordResetToken)
            .HasColumnName("pwd_reset_token")
            .HasMaxLength(200);

        builder.Property(u => u.PasswordResetTokenExpiresAt)
            .HasColumnName("pwd_reset_expires_at");

        builder.Property(u => u.TokenVersion)
            .HasColumnName("token_version")
            .HasDefaultValue(0)
            .IsRequired()
            .IsConcurrencyToken();
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YouDj.Domain.Features.Guests;

namespace YouDj.Infrastructure.Persistence.Configurations;

public sealed class GuestConfiguration : IEntityTypeConfiguration<Guest>
{
    public void Configure(EntityTypeBuilder<Guest> builder)
    {
        builder.ToTable("guests");

        builder.HasKey(g => g.Id);

        builder.Property(g => g.DisplayName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(g => g.Credits)
            .IsRequired();

        builder.Property(g => g.Phone)
            .HasMaxLength(20);

        builder.Property(g => g.PhoneVerified)
            .IsRequired();

        builder.Property(g => g.CreatedAt)
            .IsRequired();

        builder.HasIndex(g => g.Phone)
            .IsUnique(false);
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YouDj.Domain.Features.Payments;

namespace YouDj.Infrastructure.Persistence.Configurations;

public sealed class PixPaymentConfiguration
    : IEntityTypeConfiguration<PixPayment>
{
    public void Configure(EntityTypeBuilder<PixPayment> builder)
    {
        builder.ToTable("pix_payments");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.GuestId)
            .IsRequired();

        builder.Property(p => p.DjId)
            .IsRequired();

        builder.Property(p => p.Phone)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(p => p.Credits)
            .IsRequired();

        builder.Property(p => p.TotalAmount)
            .IsRequired()
            .HasPrecision(10, 2);

        builder.Property(p => p.PlatformFee)
            .IsRequired()
            .HasPrecision(10, 2);

        builder.Property(p => p.DjAmount)
            .IsRequired()
            .HasPrecision(10, 2);

        builder.Property(p => p.Status)
            .IsRequired();

        builder.Property(p => p.CreatedAt)
            .IsRequired();

        builder.HasIndex(p => p.GuestId);
        builder.HasIndex(p => p.DjId);
        builder.HasIndex(p => p.Status);
        builder.HasIndex(p => p.CreatedAt);
    }
}
using YouDj.Domain.Features.Common;

namespace YouDj.Domain.Features.Payments;

public sealed class PixPayment : EntityBase
{
    public Guid GuestId { get; private set; }
    public Guid DjId { get; private set; }

    public string Phone { get; private set; } = string.Empty;
    public int Credits { get; private set; }

    public decimal TotalAmount { get; private set; }
    public decimal PlatformFee { get; private set; }
    public decimal DjAmount { get; private set; }

    public PixPaymentStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private PixPayment() { }

    public PixPayment(Guid guestId, Guid djId, string phone, int credits,
        decimal totalAmount, decimal platformFee, decimal djAmount)
    {
        GuestId = guestId;
        DjId = djId;
        Phone = phone;
        Credits = credits;
        TotalAmount = totalAmount;
        PlatformFee = platformFee;
        DjAmount = djAmount;
        Status = PixPaymentStatus.Pending;
        CreatedAt = DateTime.UtcNow;
    }

    public void MarkAsPaid()
    {
        if (Status != PixPaymentStatus.Pending)
            throw new InvalidOperationException("Pagamento já processado.");

        Status = PixPaymentStatus.Paid;
    }

    public void Expire()
    {
        if (Status == PixPaymentStatus.Paid)
            throw new InvalidOperationException("Pagamento pago não pode expirar.");

        Status = PixPaymentStatus.Expired;
    }
}
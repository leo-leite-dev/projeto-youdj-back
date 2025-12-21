namespace YouDj.Domain.Features.Payments;

public sealed class CreditPackagePromotion
{
    public int Credits { get; }
    public decimal PromotionalPrice { get; }
    public DateTime StartsAtUtc { get; }
    public DateTime EndsAtUtc { get; }

    public CreditPackagePromotion(
        int credits, decimal promotionalPrice, DateTime startsAtUtc, DateTime endsAtUtc)
    {
        Credits = credits;
        PromotionalPrice = promotionalPrice;
        StartsAtUtc = startsAtUtc;
        EndsAtUtc = endsAtUtc;
    }

    public bool IsActive(DateTime nowUtc)
        => nowUtc >= StartsAtUtc && nowUtc <= EndsAtUtc;
}
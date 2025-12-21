namespace YouDj.Domain.Features.Payments;

public sealed class CreditPackage
{
    public int Credits { get; }
    public decimal BasePrice { get; }

    public CreditPackage(int credits, decimal basePrice)
    {
        Credits = credits;
        BasePrice = basePrice;
    }
}
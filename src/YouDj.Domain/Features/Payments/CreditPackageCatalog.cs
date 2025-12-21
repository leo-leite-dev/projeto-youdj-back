namespace YouDj.Domain.Features.Payments;

public static class CreditPackageCatalog
{
    private static readonly IReadOnlyList<CreditPackage> Packages =
        new List<CreditPackage>
        {
            new(120, 10.00m),
            new(280, 20.00m),
            new(450, 30.00m),
            new(850, 50.00m),
            new(1400, 75.00m),
            new(2000, 100.00m)
        };

    private static readonly IReadOnlyList<CreditPackagePromotion> Promotions =
        new List<CreditPackagePromotion>
        {
            // Exemplo (pode começar vazio)
            // new(
            //     credits: 1400,
            //     promotionalPrice: 40.00m,
            //     startsAtUtc: new DateTime(2025, 12, 20, 18, 0, 0, DateTimeKind.Utc),
            //     endsAtUtc: new DateTime(2025, 12, 21, 6, 0, 0, DateTimeKind.Utc)
            // )
        };

    public static bool TryGetPrice(
        int credits,
        DateTime nowUtc,
        out decimal price)
    {
        var package = Packages.FirstOrDefault(p => p.Credits == credits);

        if (package is null)
        {
            price = default;
            return false;
        }

        var promo = Promotions
            .FirstOrDefault(p =>
                p.Credits == credits &&
                p.IsActive(nowUtc));

        price = promo?.PromotionalPrice ?? package.BasePrice;
        return true;
    }
}

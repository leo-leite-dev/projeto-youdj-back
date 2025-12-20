using YouDj.Domain.Features.Common;

namespace YouDj.Domain.Features.Guests;

public sealed class Guest : EntityBase
{
    public string DisplayName { get; private set; } = string.Empty;
    public int Credits { get; private set; }
    public string? Phone { get; private set; }
    public bool PhoneVerified { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Guest() { }

    public Guest(string displayName)
    {
        DisplayName = displayName;
        Credits = 0;
        CreatedAt = DateTime.UtcNow;
    }

    public void AddCredits(int amount)
    {
        Credits += amount;
    }

    public bool TrySpendCredits(int amount)
    {
        if (Credits < amount)
            return false;

        Credits -= amount;
        return true;
    }

    public void AttachPhone(string phone)
    {
        Phone = phone;
        PhoneVerified = false;
    }

    public void VerifyPhone()
    {
        PhoneVerified = true;
    }
}
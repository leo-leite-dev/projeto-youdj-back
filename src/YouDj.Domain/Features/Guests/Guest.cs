using YouDj.Domain.Features.Common;

namespace YouDj.Domain.Features.Guests;

public sealed class Guest : EntityBase
{
    public string DisplayName { get; private set; } = string.Empty;
    public int Credits { get; private set; }

    public string? Phone { get; private set; }
    public bool PhoneVerified { get; private set; }

    public GuestStatus Status { get; private set; }

    private Guest() { }

    public Guest(string displayName)
    {
        DisplayName = displayName;
        Credits = 0;
        Status = GuestStatus.Pending;
    }

    public void ActivateGuest()
    {
        if (Status != GuestStatus.Pending)
            throw new InvalidOperationException("Guest não pode ser ativado neste estado.");

        Status = GuestStatus.Active;
        Touch();
    }

    public void ExpireGuest()
    {
        if (Status == GuestStatus.Expired)
            return;

        Status = GuestStatus.Expired;
        Touch();
    }

    public bool CanUseSystem()
        => IsActive && Status == GuestStatus.Active;

    public void AddCredits(int amount)
    {
        if (amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount));

        Credits += amount;
        Touch();
    }

    public bool TrySpendCredits(int amount)
    {
        if (!CanUseSystem())
            return false;

        if (Credits < amount)
            return false;

        Credits -= amount;
        Touch();
        return true;
    }

    public void AttachPhone(string phone)
    {
        Phone = phone;
        PhoneVerified = false;
        Touch();
    }

    public void VerifyPhone()
    {
        if (Phone is null)
            throw new InvalidOperationException("Não há telefone para verificar.");

        PhoneVerified = true;
        Touch();
    }
}
namespace YouDj.Domain.Features.Common;

public abstract class EntityBase
{
    public Guid Id { get; private set; } = Guid.NewGuid();

    public DateTimeOffset CreatedAtUtc { get; private set; }
    public DateTimeOffset? UpdatedAtUtc { get; private set; }

    public bool IsActive { get; private set; } = true;

    protected EntityBase()
    {
        CreatedAtUtc = DateTimeOffset.UtcNow;
    }

    public void Touch() => UpdatedAtUtc = DateTimeOffset.UtcNow;

    public void Activate()
    {
        if (!IsActive)
        {
            IsActive = true;
            Touch();
        }
    }

    public void Desactivate()
    {
        if (IsActive)
        {
            IsActive = false;
            Touch();
        }
    }
}
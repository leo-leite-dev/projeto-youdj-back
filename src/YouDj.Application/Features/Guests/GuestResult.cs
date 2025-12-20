namespace YouDj.Application.Features.Guests;

public sealed record GuestResult
{
    public required Guid GuestId { get; init; }
    public required string DisplayName { get; init; }
    public required int Credits { get; init; }
}
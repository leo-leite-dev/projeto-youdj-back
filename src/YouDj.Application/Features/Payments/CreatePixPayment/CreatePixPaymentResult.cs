namespace YouDj.Application.Features.Payments.CreatePixPayment;

public sealed record CreatePixPaymentResult
{
    public required Guid PaymentId { get; init; }
    public required decimal Amount { get; init; }
    public required string Message { get; init; }
}

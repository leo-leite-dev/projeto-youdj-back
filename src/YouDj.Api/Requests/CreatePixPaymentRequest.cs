namespace YouDj.Api.Requests;

public sealed record CreatePixPaymentRequest
{
    public required int Credits { get; init; }
    public required string Phone { get; init; }
}

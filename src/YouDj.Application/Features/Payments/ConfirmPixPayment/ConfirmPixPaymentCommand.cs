using MediatR;
using YouDj.Application.Common.Results;

namespace YouDj.Application.Features.Payments;

public sealed record ConfirmPixPaymentCommand : IRequest<Result>
{
    public required Guid PaymentId { get; init; }
}
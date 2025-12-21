using MediatR;
using YouDj.Application.Common.Results;

namespace YouDj.Application.Features.Payments.CreatePixPayment;

public sealed class CreatePixPaymentCommand : IRequest<Result<CreatePixPaymentResult>>
{
    public required string SessionToken { get; init; }
    public required int Credits { get; init; }
    public required string Phone { get; init; }
}
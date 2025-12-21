using MediatR;
using YouDj.Application.Common.Results;
using YouDj.Application.Features.Repositories;
using YouDj.Application.Abstractions.Persistences;
using YouDj.Domain.Features.Payments;

namespace YouDj.Application.Features.Payments;

public sealed class ConfirmPixPaymentHandler
    : IRequestHandler<ConfirmPixPaymentCommand, Result>
{
    private readonly IPixPaymentRepository _paymentRepository;
    private readonly IGuestRepository _guestRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ConfirmPixPaymentHandler(
        IPixPaymentRepository paymentRepository,
        IGuestRepository guestRepository,
        IUnitOfWork unitOfWork)
    {
        _paymentRepository = paymentRepository;
        _guestRepository = guestRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(
        ConfirmPixPaymentCommand command,
        CancellationToken ct)
    {
        var payment = await _paymentRepository.GetByIdAsync(command.PaymentId, ct);

        if (payment is null)
            return Result.NotFound("Pagamento não encontrado.");

        if (payment.Status != PixPaymentStatus.Pending)
            return Result.Conflict("Pagamento já foi processado.");

        var guest = await _guestRepository
            .GetByIdAsync(payment.GuestId, ct);

        if (guest is null)
            return Result.ServerError(
                "Guest associado ao pagamento não encontrado."
            );

        payment.MarkAsPaid();

        guest.AddCredits(payment.Credits);

        guest.VerifyPhone();

        await _unitOfWork.CommitAsync(ct);

        return Result.Ok("Pagamento confirmado com sucesso.");
    }
}
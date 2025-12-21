using MediatR;
using YouDj.Application.Common.Results;
using YouDj.Application.Features.Repositories;
using YouDj.Domain.Features.Payments;
using YouDj.Application.Abstractions.Persistences;
using YouDj.Domain.Features.Guests;
using YouDj.Application.Abstractions.Auth;

namespace YouDj.Application.Features.Payments.CreatePixPayment;

public sealed class CreatePixPaymentHandler
    : IRequestHandler<CreatePixPaymentCommand, Result<CreatePixPaymentResult>>
{
    private readonly IGuestRepository _guestRepository;
    private readonly IPixPaymentRepository _paymentRepository;
    private readonly IGuestSessionReader _sessionReader;
    private readonly IUnitOfWork _unitOfWork;

    private const decimal PlatformPercentage = 0.20m;

    public CreatePixPaymentHandler(
        IGuestRepository guestRepository,
        IPixPaymentRepository paymentRepository,
        IGuestSessionReader sessionReader,
        IUnitOfWork unitOfWork)
    {
        _guestRepository = guestRepository;
        _paymentRepository = paymentRepository;
        _sessionReader = sessionReader;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CreatePixPaymentResult>> Handle(
        CreatePixPaymentCommand command, CancellationToken ct)
    {
        var session = _sessionReader.Read(command.SessionToken);

        if (session is null)
            return Result<CreatePixPaymentResult>.Unauthorized("Sessão inválida.");

        var guest = new Guest(session.DisplayName);
        guest.AttachPhone(command.Phone);

        await _guestRepository.AddAsync(guest, ct);

        if (!CreditPackageCatalog.TryGetPrice(
                command.Credits,
                DateTime.UtcNow,
                out var totalAmount))
        {
            return Result<CreatePixPaymentResult>.BadRequest(
                "Pacote de créditos inválido."
            );
        }

        var platformFee = Math.Round(totalAmount * PlatformPercentage, 2);
        var djAmount = totalAmount - platformFee;

        var payment = new PixPayment(
            guestId: guest.Id,
            djId: session.DjId,
            phone: command.Phone,
            credits: command.Credits,
            totalAmount: totalAmount,
            platformFee: platformFee,
            djAmount: djAmount
        );

        await _paymentRepository.AddAsync(payment, ct);
        await _unitOfWork.CommitAsync(ct);

        return Result<CreatePixPaymentResult>.Ok(new CreatePixPaymentResult
        {
            PaymentId = payment.Id,
            Amount = totalAmount,
            Message = "Pagamento PIX criado. Aguardando pagamento."
        });
    }
}
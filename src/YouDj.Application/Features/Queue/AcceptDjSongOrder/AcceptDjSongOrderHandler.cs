using MediatR;
using YouDj.Application.Common.Results;
using YouDj.Application.Abstractions.Repositories;
using YouDj.Application.Abstractions.Persistences;
using YouDj.Domain.Queue;
using YouDj.Application.Features.Repositories;
using YouDj.Domain.Features.Common.Enums;

namespace YouDj.Application.Features.SongOrders.Accept;

public sealed class AcceptDjSongOrderHandler
    : IRequestHandler<AcceptDjSongOrderCommand, Result<Guid>>
{
    private readonly ISongOrderRepository _orderRepository;
    private readonly IGuestRepository _guestRepository;
    private readonly IQueueRepository _queueRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AcceptDjSongOrderHandler(
        ISongOrderRepository orderRepository,
        IGuestRepository guestRepository,
        IQueueRepository queueRepository,
        IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _guestRepository = guestRepository;
        _queueRepository = queueRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(
        AcceptDjSongOrderCommand command, CancellationToken ct)
    {
        var order = await _orderRepository.GetByIdAsync(command.OrderId, ct);

        if (order is null)
            return Result<Guid>.NotFound("Pedido não encontrado.");

        if (order.DjId != command.DjId)
            return Result<Guid>.Forbidden("DJ não autorizado.");

        if (order.Status != DjSongOrderStatus.Pending)
            return Result<Guid>.Conflict("Pedido não está pendente.");

        var guest = await _guestRepository.GetByIdAsync(order.GuestId, ct);

        if (guest is null || !guest.CanUseSystem())
            return Result<Guid>.BadRequest("Guest inválido.");

        if (!guest.TrySpendCredits(order.PriceInCredits))
            return Result<Guid>.BadRequest("Créditos insuficientes.");

        order.Accept();

        var queueItem = QueueItem.CreateFromOrder(order);

        await _queueRepository.AddAsync(queueItem, ct);
        await _unitOfWork.CommitAsync(ct);

        return Result<Guid>.Ok(queueItem.Id);
    }
}
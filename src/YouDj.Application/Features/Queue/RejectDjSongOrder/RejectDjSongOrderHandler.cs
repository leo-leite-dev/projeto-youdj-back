using MediatR;
using YouDj.Application.Common.Results;
using YouDj.Application.Abstractions.Repositories;
using YouDj.Application.Abstractions.Persistences;

namespace YouDj.Application.Features.SongOrders.Reject;

public sealed class RejectDjSongOrderHandler
    : IRequestHandler<RejectDjSongOrderCommand, Result>
{
    private readonly ISongOrderRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public RejectDjSongOrderHandler(
        ISongOrderRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(
        RejectDjSongOrderCommand command,
        CancellationToken ct)
    {
        var order = await _repository.GetByIdAsync(command.OrderId, ct);

        if (order is null)
            return Result.NotFound("Pedido não encontrado.");

        if (order.DjId != command.DjId)
            return Result.Forbidden("DJ não autorizado.");

        order.Reject();

        await _unitOfWork.CommitAsync(ct);

        return Result.Ok("Pedido rejeitado.");
    }
}

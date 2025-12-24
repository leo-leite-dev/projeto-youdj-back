using MediatR;
using YouDj.Application.Common.Results;
using YouDj.Domain.SongOrders;
using YouDj.Application.Abstractions.Repositories;
using YouDj.Domain.Features.Common.Exceptions;

namespace YouDj.Application.Features.SongOrders.Create;

public sealed class CreateDjSongOrderHandler
    : IRequestHandler<CreateDjSongOrderCommand, Result<Guid>>
{
    private readonly ISongOrderRepository _repository;

    public CreateDjSongOrderHandler(ISongOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<Guid>> Handle(
        CreateDjSongOrderCommand command,
        CancellationToken ct)
    {
        DjSongOrder order;

        try
        {
            order = DjSongOrder.Create(
                command.DjId,
                command.GuestId,
                command.PriceInCredits,
                command.ExternalId,
                command.Title,
                command.ThumbnailUrl,
                command.Source,
                command.Duration
            );
        }
        catch (DomainException ex)
        {
            return Result<Guid>.BadRequest(ex.Message);
        }

        await _repository.AddAsync(order, ct);

        return Result<Guid>.Ok(order.Id);
    }
}

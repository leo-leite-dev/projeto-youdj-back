using MediatR;
using YouDj.Application.Common.Results;

namespace YouDj.Application.Features.SongOrders.Accept;

public sealed record AcceptDjSongOrderCommand : IRequest<Result<Guid>>
{
    public required Guid OrderId { get; init; }
    public required Guid DjId { get; init; }
}
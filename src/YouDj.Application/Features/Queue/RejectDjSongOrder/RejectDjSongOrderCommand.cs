using MediatR;
using YouDj.Application.Common.Results;

namespace YouDj.Application.Features.SongOrders.Reject;

public sealed record RejectDjSongOrderCommand : IRequest<Result>
{
    public required Guid OrderId { get; init; }
    public required Guid DjId { get; init; }
}
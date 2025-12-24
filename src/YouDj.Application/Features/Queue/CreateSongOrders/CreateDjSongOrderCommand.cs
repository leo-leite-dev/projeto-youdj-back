using MediatR;
using YouDj.Application.Common.Results;

namespace YouDj.Application.Features.SongOrders.Create;

public sealed record CreateDjSongOrderCommand : IRequest<Result<Guid>>
{
    public required Guid DjId { get; init; }
    public required Guid GuestId { get; init; }

    public required int PriceInCredits { get; init; }

    public required string ExternalId { get; init; }
    public required string Title { get; init; }
    public required string ThumbnailUrl { get; init; }
    public required string Source { get; init; }

    public TimeSpan? Duration { get; init; }
}
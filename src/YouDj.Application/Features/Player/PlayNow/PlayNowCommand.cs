using MediatR;
using YouDj.Application.Common.Results;

namespace YouDj.Application.Features.Player;

public sealed record PlayNowCommand : IRequest<Result>
{
    public required Guid DjId { get; init; }
    public required string ExternalId { get; init; }
    public required string Title { get; init; }
    public required string ThumbnailUrl { get; init; }
    public required string Source { get; init; }
}
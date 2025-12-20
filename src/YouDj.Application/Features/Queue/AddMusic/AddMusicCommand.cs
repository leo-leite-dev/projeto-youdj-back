using MediatR;
using YouDj.Application.Common.Results;

namespace YouDj.Application.Features.Queue.AddMusic;

public sealed record AddMusicCommand : IRequest<Result<Guid>>
{
    public required Guid DjId { get; init; }
    public required string ExternalId { get; init; }
    public required string Title { get; init; }
    public TimeSpan? Duration { get; init; }
    public required string ThumbnailUrl { get; init; }
    public required string Source { get; init; }
}
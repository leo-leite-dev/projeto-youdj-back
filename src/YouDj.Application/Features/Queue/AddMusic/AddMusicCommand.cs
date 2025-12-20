using MediatR;
using YouDj.Application.Common.Results;

namespace YouDj.Application.Features.Queue;

public sealed record AddMusicCommand(
    Guid DjId,
    string ExternalId,
    string Title,
    TimeSpan? Duration,
    string ThumbnailUrl,
    string Source
) : IRequest<Result>;
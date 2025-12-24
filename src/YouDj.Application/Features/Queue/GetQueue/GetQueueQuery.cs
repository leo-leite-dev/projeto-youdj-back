using MediatR;
using YouDj.Application.Common.Results;

namespace YouDj.Application.Features.Queue.GetQueue;

public sealed record GetQueueQuery(Guid DjId)
    : IRequest<Result<IReadOnlyList<QueueItemResponse>>>;

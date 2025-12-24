using MediatR;
using YouDj.Application.Common.Results;

namespace YouDj.Application.Features.Player.PlayerFromQueue;

public sealed record PlayFromQueueCommand(Guid DjId)
    : IRequest<Result<Unit>>;

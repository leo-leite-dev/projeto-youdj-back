using MediatR;
using YouDj.Application.Common.Results;

namespace YouDj.Application.Features.Player.FinishPlaying;

public sealed record FinishPlayingCommand(Guid DjId)
    : IRequest<Result<Unit>>;

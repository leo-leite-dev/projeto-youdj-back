using MediatR;

namespace YouDj.Application.Features.Player;

public sealed record GetCurrentPlayingQuery(Guid DjId)
    : IRequest<CurrentPlayingDto?>;

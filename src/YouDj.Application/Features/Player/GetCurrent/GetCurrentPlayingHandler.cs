using MediatR;
using YouDj.Application.Abstractions.Repositories;

namespace YouDj.Application.Features.Player;

public sealed class GetCurrentPlayingHandler
    : IRequestHandler<GetCurrentPlayingQuery, CurrentPlayingDto?>
{
    private readonly INowPlayingRepository _nowPlayingRepository;

    public GetCurrentPlayingHandler(
        INowPlayingRepository nowPlayingRepository)
    {
        _nowPlayingRepository = nowPlayingRepository;
    }

    public async Task<CurrentPlayingDto?> Handle(
        GetCurrentPlayingQuery request, CancellationToken ct)
    {
        var current = await _nowPlayingRepository
            .GetByDjIdAsync(request.DjId, ct);

        if (current is null)
            return null;

        var track = current.Track;

        return new CurrentPlayingDto(
            track.ExternalId,
            track.Title,
            track.ThumbnailUrl,
            track.Source
        );
    }
}
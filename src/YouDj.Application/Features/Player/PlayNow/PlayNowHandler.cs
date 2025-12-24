using MediatR;
using YouDj.Application.Common.Results;
using YouDj.Application.Abstractions.Repositories;
using YouDj.Domain.Features.Common.ValueObjects;
using YouDj.Application.Abstractions.Persistences;
using YouDj.Domain.Player;

namespace YouDj.Application.Features.Player.PlayNow;

public sealed class PlayNowHandler
    : IRequestHandler<PlayNowCommand, Result>
{
    private readonly IDjRepository _djRepository;
    private readonly INowPlayingRepository _nowPlayingRepository;
    private readonly IUnitOfWork _unitOfWork;

    public PlayNowHandler(
        IDjRepository djRepository,
        INowPlayingRepository nowPlayingRepository,
        IUnitOfWork unitOfWork)
    {
        _djRepository = djRepository;
        _nowPlayingRepository = nowPlayingRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(
        PlayNowCommand command, CancellationToken ct)
    {
        var dj = await _djRepository.GetByIdAsync(command.DjId, ct);

        if (dj is null)
            return Result.NotFound("DJ não encontrado.");

        var track = TrackInfo.Create(
            command.ExternalId,
            command.Title,
            command.ThumbnailUrl,
            command.Source
        );

        var current = await _nowPlayingRepository
            .GetByDjIdAsync(command.DjId, ct);

        if (current is not null)
            _nowPlayingRepository.Remove(current);

        var nowPlaying = NowPlaying.Start(dj.Id, track);

        await _nowPlayingRepository.AddAsync(nowPlaying, ct);
        await _unitOfWork.CommitAsync(ct);

        return Result.Ok();
    }

}
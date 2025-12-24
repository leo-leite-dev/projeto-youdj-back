using MediatR;
using YouDj.Application.Common.Results;
using YouDj.Application.Abstractions.Repositories;
using YouDj.Application.Abstractions.Persistences;
using YouDj.Domain.Player;

namespace YouDj.Application.Features.Player.FinishPlaying;

public sealed class FinishPlayingHandler
    : IRequestHandler<FinishPlayingCommand, Result<Unit>>
{
    private readonly INowPlayingRepository _nowPlayingRepository;
    private readonly IQueueRepository _queueRepository;
    private readonly IUnitOfWork _unitOfWork;

    public FinishPlayingHandler(
        INowPlayingRepository nowPlayingRepository,
        IQueueRepository queueRepository,
        IUnitOfWork unitOfWork)
    {
        _nowPlayingRepository = nowPlayingRepository;
        _queueRepository = queueRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Unit>> Handle(
        FinishPlayingCommand command,
        CancellationToken ct)
    {
        var current = await _nowPlayingRepository
            .GetByDjIdAsync(command.DjId, ct);

        if (current is null)
            return Result<Unit>.NoContent();

        _nowPlayingRepository.Remove(current);

        var next = await _queueRepository
            .GetFirstAsync(command.DjId, ct);

        if (next is not null)
        {
            var nowPlaying = NowPlaying.Start(
                command.DjId,
                next.Track
            );

            await _nowPlayingRepository.AddAsync(nowPlaying, ct);

            _queueRepository.Remove(next);
        }

        await _unitOfWork.CommitAsync(ct);

        return Result<Unit>.Ok();
    }
}
using MediatR;
using YouDj.Application.Common.Results;
using YouDj.Application.Abstractions.Repositories;
using YouDj.Application.Abstractions.Persistences;
using YouDj.Domain.Player;

namespace YouDj.Application.Features.Player.PlayerFromQueue;

public sealed class PlayFromQueueHandler
    : IRequestHandler<PlayFromQueueCommand, Result<Unit>>
{
    private readonly IDjRepository _djRepository;
    private readonly INowPlayingRepository _nowPlayingRepository;
    private readonly IQueueRepository _queueRepository;
    private readonly IUnitOfWork _unitOfWork;

    public PlayFromQueueHandler(
        IDjRepository djRepository,
        INowPlayingRepository nowPlayingRepository,
        IQueueRepository queueRepository,
        IUnitOfWork unitOfWork)
    {
        _djRepository = djRepository;
        _nowPlayingRepository = nowPlayingRepository;
        _queueRepository = queueRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Unit>> Handle(
        PlayFromQueueCommand command,
        CancellationToken ct)
    {
        var dj = await _djRepository.GetByIdAsync(command.DjId, ct);

        if (dj is null)
            return Result<Unit>.NotFound("DJ não encontrado.");

        var current = await _nowPlayingRepository
            .GetByDjIdAsync(command.DjId, ct);

        if (current is not null)
            return Result<Unit>.NoContent();

        var next = await _queueRepository
            .GetFirstAsync(command.DjId, ct);

        if (next is null)
            return Result<Unit>.NoContent();

        var nowPlaying = NowPlaying.Start(
            dj.Id,
            next.Track
        );

        await _nowPlayingRepository.AddAsync(nowPlaying, ct);
        _queueRepository.Remove(next);
        await _unitOfWork.CommitAsync(ct);

        return Result<Unit>.Ok();
    }
}
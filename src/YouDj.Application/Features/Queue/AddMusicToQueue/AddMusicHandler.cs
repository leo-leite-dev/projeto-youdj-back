using MediatR;
using YouDj.Application.Common.Results;
using YouDj.Application.Abstractions.Repositories;
using YouDj.Domain.Queue;
using YouDj.Domain.Features.Queue.Exceptions;
using YouDj.Domain.Features.Common.ValueObjects;
using YouDj.Application.Abstractions.Persistences;

namespace YouDj.Application.Features.Queue.AddMusicToQueue;

public sealed class AddMusicToQueueHandler
    : IRequestHandler<AddMusicToQueueCommand, Result<Guid>>
{
    private readonly IDjRepository _djRepository;
    private readonly IQueueRepository _queueRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddMusicToQueueHandler(
        IDjRepository djRepository,
        IQueueRepository queueRepository,
        IUnitOfWork unitOfWork)
    {
        _djRepository = djRepository;
        _queueRepository = queueRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(
        AddMusicToQueueCommand command, CancellationToken ct)
    {
        var dj = await _djRepository.GetByIdAsync(command.DjId, ct);

        if (dj is null)
            return Result<Guid>.NotFound("DJ não encontrado.");

        if (dj.ActivePlaylistId is null)
            return Result<Guid>.BadRequest("DJ não possui playlist ativa.");

        QueueItem item;

        try
        {
            var track = TrackInfo.Create(
                command.ExternalId,
                command.Title,
                command.ThumbnailUrl,
                command.Source
            );

            item = QueueItem.CreateByDj(
                djId: dj.Id,
                track: track,
                duration: command.Duration
            );
        }
        catch (Exception ex) when (ex is QueueException || ex is ArgumentException)
        {
            return Result<Guid>.BadRequest(ex.Message);
        }

        var lastPosition = await _queueRepository
            .GetLastPositionAsync(dj.Id, ct);

        item.SetPosition(lastPosition + 1);

        await _queueRepository.AddAsync(item, ct);
        await _unitOfWork.CommitAsync(ct);

        return Result<Guid>.Ok(item.Id);
    }
}
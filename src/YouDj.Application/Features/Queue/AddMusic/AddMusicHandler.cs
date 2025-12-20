using MediatR;
using YouDj.Application.Common.Results;
using YouDj.Application.Abstractions.Repositories;
using YouDj.Domain.Queue;
using YouDj.Domain.Features.Queue.Exceptions;

namespace YouDj.Application.Features.Queue.AddMusic;

public sealed class AddMusicHandler
    : IRequestHandler<AddMusicCommand, Result<Guid>>
{
    private readonly IUserRepository _userRepository;
    private readonly IQueueRepository _queueRepository;

    public AddMusicHandler(
        IUserRepository userRepository,
        IQueueRepository queueRepository)
    {
        _userRepository = userRepository;
        _queueRepository = queueRepository;
    }

    public async Task<Result<Guid>> Handle(
        AddMusicCommand command,
        CancellationToken ct)
    {
        var user = await _userRepository.GetByIdAsync(command.DjId, ct);

        if (user is null)
            return Result<Guid>.NotFound("DJ não encontrado.");

        if (user.ActivePlaylistId is null)
            return Result<Guid>.BadRequest("DJ não possui playlist ativa.");

        QueueItem item;

        try
        {
            item = QueueItem.Create(
                djId: user.Id,
                externalId: command.ExternalId,
                title: command.Title,
                thumbnailUrl: command.ThumbnailUrl,
                source: command.Source,
                duration: command.Duration
            );
        }
        catch (QueueException ex)
        {
            return Result<Guid>.BadRequest(ex.Message);
        }

        await _queueRepository.AddAsync(item, ct);

        return Result<Guid>.Ok(item.Id);
    }
}
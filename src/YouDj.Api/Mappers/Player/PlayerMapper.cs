using YouDj.Api.Contracts.Player;
using YouDj.Application.Features.Player;
using YouDj.Application.Features.Player.FinishPlaying;
using YouDj.Application.Features.Player.PlayerFromQueue;

namespace YouDj.Api.Mappers.Player;

public static class PlayerMapper
{
    public static PlayNowCommand ToCommand(
        this PlayNowRequest req,
        Guid djId)
        => new()
        {
            DjId = djId,
            ExternalId = req.ExternalId,
            Title = req.Title,
            ThumbnailUrl = req.ThumbnailUrl,
            Source = req.Source
        };

    public static PlayFromQueueCommand ToCommand(
        this PlayFromQueueRequest _,
        Guid djId)
        => new(djId);

    public static CurrentPlayingResponse ToResponse(
        this CurrentPlayingDto dto)
        => new(
            dto.ExternalId,
            dto.Title,
            dto.ThumbnailUrl,
            dto.Source
        );

    public static FinishPlayingCommand ToCommand(
        this FinishPlayingRequest _,
        Guid djId)
        => new(djId);
}
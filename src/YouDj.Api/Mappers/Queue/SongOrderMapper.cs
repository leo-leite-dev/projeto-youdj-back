using YouDj.Api.Helpers;
using YouDj.Api.Requests.Queue;
using YouDj.Application.Features.SongOrders.Create;

namespace YouDj.Api.Mappers;

public static class SongOrderMapper
{
    public static CreateDjSongOrderCommand ToCommand(
        this CreateDjSongOrderRequest request,
        HttpContext httpContext)
    {
        return new CreateDjSongOrderCommand
        {
            DjId = request.DjId,
            GuestId = httpContext.GetGuestId(),
            PriceInCredits = request.PriceInCredits,
            ExternalId = request.ExternalId,
            Title = request.Title,
            ThumbnailUrl = request.ThumbnailUrl,
            Source = request.Source,
            Duration = request.Duration
        };
    }
}
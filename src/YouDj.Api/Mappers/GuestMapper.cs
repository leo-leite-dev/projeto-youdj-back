using YouDj.Api.Requests;
using YouDj.Application.Features.Guests;

namespace YouDj.Api.Mappers;

public static class GuestMapper
{
    public static CreateGuestCommand ToCommand(
        this CreateGuestRequest request)
        => new()
        {
            DisplayName = request.DisplayName
        };
}
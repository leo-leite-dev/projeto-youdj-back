using System.Security.Claims;

namespace YouDj.Api.Helpers;

public static class HttpContextExtensions
{
    public static Guid GetGuestId(this HttpContext context)
    {
        var guestIdClaim = context.User
            .FindFirst("guest_id");

        if (guestIdClaim is null)
            throw new InvalidOperationException("GuestId não encontrado no token.");

        return Guid.Parse(guestIdClaim.Value);
    }

    public static Guid GetDjId(this HttpContext context)
    {
        var djIdClaim = context.User
            .FindFirst(ClaimTypes.NameIdentifier);

        if (djIdClaim is null)
            throw new InvalidOperationException("DjId não encontrado no token.");

        return Guid.Parse(djIdClaim.Value);
    }
}
using YouDj.Application.Features.Auth.Guest;

namespace YouDj.Application.Abstractions.Auth;

public interface IGuestTokenService
{
    GuestTokenResult Issue(Guid guestId);
}

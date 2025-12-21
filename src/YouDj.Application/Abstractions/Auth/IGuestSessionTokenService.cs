using YouDj.Application.Features.Auth.Session;

namespace YouDj.Application.Abstractions.Auth;

public interface IGuestSessionTokenService
{
    GuestSessionResult Issue(string displayName, Guid djId);
}
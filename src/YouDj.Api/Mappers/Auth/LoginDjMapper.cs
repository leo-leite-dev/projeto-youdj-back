using YouDj.Application.Features.Auth.Login.Dj;
using YouDj.Api.Contracts.Auth;

namespace YouDj.Api.Mappers.Auth;

public static class LoginDjMapper
{
    public static LoginCommand ToCommand(this LoginDjRequest request)
        => new()
        {
            Identify = request.Identify,
            Password = request.Password
        };

    public static LoginResponse ToResponse(this LoginResult result)
        => new()
        {
            DjId = result.DjId,
            ExpiresAtUtc = result.ExpiresAtUtc,
            IsDj = result.IsDj
        };
}
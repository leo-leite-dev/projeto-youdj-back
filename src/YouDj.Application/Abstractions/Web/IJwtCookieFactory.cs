using YouDj.Application.Abstractions.Auth;

namespace YouDj.Application.Abstractions.Web;

public interface IJwtCookieFactory
{
    JwtCookie CreateLoginCookie(string token, TimeSpan ttl);
    JwtCookie CreateLogoutCookie();
}

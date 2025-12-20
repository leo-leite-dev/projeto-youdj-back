using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using YouDj.Application.Abstractions.Auth;
using YouDj.Domain.Features.Common.ValueObjects;

namespace YouDj.Infrastructure.Auth;

public sealed class CurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor _http;

    public CurrentUser(IHttpContextAccessor http)
    {
        _http = http;
    }

    public bool IsAuthenticated =>
        _http.HttpContext?.User?.Identity?.IsAuthenticated == true;

    public Guid UserId
    {
        get
        {
            var raw =
                _http.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? _http.HttpContext?.User?.FindFirstValue("sub");

            return Guid.TryParse(raw, out var id)
                ? id
                : Guid.Empty;
        }
    }

    public Username Username
    {
        get
        {
            var raw =
                _http.HttpContext?.User?.FindFirstValue(ClaimTypes.Name)
                ?? _http.HttpContext?.User?.Identity?.Name;

            return Username.TryParse(raw, out var username)
                ? username
                : Username.Empty;
        }
    }

    public string? Email =>
        _http.HttpContext?.User?.FindFirstValue(ClaimTypes.Email);
}
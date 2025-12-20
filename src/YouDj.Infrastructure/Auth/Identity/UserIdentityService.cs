using YouDj.Application.Abstractions.Identity;
using YouDj.Application.Features.Auth.Identity;
using YouDj.Domain.Features.Users.Entities;

namespace YouDj.Infrastructure.Auth.Identity;

public sealed class UserIdentityService : IUserIdentityService
{
    private const int MinimumDjAge = 18;

    public UserIdentity Create(User user)
    {
        var claims = new Dictionary<string, string>
        {
            ["username"] = user.Username.Value,
            ["token_version"] = user.TokenVersion.ToString(),
            ["is_dj"] = user.BirthDate.IsAdult(MinimumDjAge) ? "true" : "false"
        };

        return new UserIdentity(
            Roles: Array.Empty<string>(),
            Claims: claims
        );
    }
}
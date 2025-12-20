using YouDj.Domain.Features.Users.Entities;

namespace YouDj.Application.Auth;

public static class UserIdentityFactory
{
    public static UserIdentity Create(User user)
    {
        var isDj = user.ActivePlaylistId is not null;

        return new UserIdentity
        {
            Roles = isDj ? new[] { "dj" } : Array.Empty<string>(),
            Claims = new Dictionary<string, string>
            {
                ["is_dj"] = isDj ? "true" : "false"
            }
        };
    }

    public static UserIdentity CreateDj()
    {
        return new UserIdentity
        {
            Roles = new[] { "dj" },
            Claims = new Dictionary<string, string>
            {
                ["is_dj"] = "true"
            }
        };
    }
}
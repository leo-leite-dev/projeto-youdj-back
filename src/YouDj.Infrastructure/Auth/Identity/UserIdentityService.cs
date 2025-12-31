using YouDj.Application.Abstractions.Identity;
using YouDj.Application.Features.Dj.Auth.Identity;
using YouDj.Domain.Features.Dj.Entities.User;

namespace YouDj.Infrastructure.Auth.Identity;

public sealed class UserIdentityService : IDjIdentityService
{
    private const int MinimumDjAge = 18;

    public DjIdentity Create(UserDj user)
    {
        if (user.ActivePlaylistId is null)
            throw new InvalidOperationException(
                "DJ autenticado sem playlist ativa."
            );

        var claims = new Dictionary<string, string>
        {
            ["username"] = user.Username.Value,
            ["token_version"] = user.TokenVersion.ToString(),
            ["is_dj"] = user.BirthDate.IsAdult(MinimumDjAge) ? "true" : "false",
            ["playlist_id"] = user.ActivePlaylistId.Value.ToString() // 🔥 AQUI
        };

        return new DjIdentity(
            Roles: Array.Empty<string>(),
            Claims: claims
        );
    }
}

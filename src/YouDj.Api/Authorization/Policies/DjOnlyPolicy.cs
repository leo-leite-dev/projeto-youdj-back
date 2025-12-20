using Microsoft.AspNetCore.Authorization;

namespace YouDj.Api.Authorization.Policies;

public static class DjOnlyPolicy
{
    public const string Name = "DjOnly";

    public static AuthorizationPolicy Build()
        => new AuthorizationPolicyBuilder()
            .RequireClaim("is_dj", "true")
            .Build();
}
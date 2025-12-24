using Microsoft.AspNetCore.Authorization;

namespace YouDj.Api.Authorization.Policies;

public static class GuestOnlyPolicy
{
    public const string Name = "GuestOnly";

    public static AuthorizationPolicy Build()
        => new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .RequireClaim("scope", "session")
            .Build();
}
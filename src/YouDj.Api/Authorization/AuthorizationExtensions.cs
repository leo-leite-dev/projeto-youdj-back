using YouDj.Api.Authorization.Policies;

namespace YouDj.Api.Authorization;

public static class AuthorizationExtensions
{
    public static IServiceCollection AddApiAuthorization(
        this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy(DjOnlyPolicy.Name, DjOnlyPolicy.Build());
        });

        return services;
    }
}
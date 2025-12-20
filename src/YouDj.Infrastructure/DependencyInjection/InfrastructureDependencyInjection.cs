using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YouDj.Application.Abstractions.Auth;
using YouDj.Application.Abstractions.Repositories;
using YouDj.Application.Common.Errors;
using YouDj.Application.Interfaces.Youtube;
using YouDj.Infrastructure.Auth.Services.Jwt;
using YouDj.Infrastructure.Auth.Services.Security;
using YouDj.Infrastructure.Common.Errors;
using YouDj.Infrastructure.Data.Repositories;
using YouDj.Infrastructure.Persistence;
using YouDj.Infrastructure.Persistence.Users;
using YouDj.Infrastructure.Youtube;

namespace YouDj.Infrastructure.DependencyInjection;

public static class InfrastructureDependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<YouDjDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IQueueRepository, QueueRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IPasswordService, PasswordService>();
        services.AddScoped<IJwtTokenService, JwtTokenService>();

        services.AddScoped<IDbErrorTranslator, PostgresDbErrorTranslator>();

        services.AddHttpClient<IYoutubeApiClient, YoutubeApiClient>();

        return services;
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YouDj.Application.Abstractions.Repositories;
using YouDj.Application.Interfaces.Youtube;
using YouDj.Infrastructure.Data;
using YouDj.Infrastructure.Data.Repositories;
using YouDj.Infrastructure.Youtube;

namespace YouDj.Infrastructure.DependencyInjection;

public static class InfrastructureDependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // DbContext - PostgreSQL
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection")));

        // Repositories
        services.AddScoped<IQueueRepository, QueueRepository>();

        // External APIs
        services.AddHttpClient<IYoutubeApiClient, YoutubeApiClient>();

        return services;
    }
}
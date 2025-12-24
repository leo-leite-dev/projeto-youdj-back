using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YouDj.Application.Abstractions.Auth;
using YouDj.Application.Abstractions.Identity;
using YouDj.Application.Abstractions.Persistences;
using YouDj.Application.Abstractions.Repositories;
using YouDj.Application.Abstractions.Web;
using YouDj.Application.Common.Errors;
using YouDj.Application.Features.Repositories;
using YouDj.Application.Interfaces.Youtube;
using YouDj.Infrastructure.Auth;
using YouDj.Infrastructure.Auth.Identity;
using YouDj.Infrastructure.Auth.Services.Cookies;
using YouDj.Infrastructure.Auth.Services.Jwt;
using YouDj.Infrastructure.Auth.Services.Security;
using YouDj.Infrastructure.Common.Errors;
using YouDj.Infrastructure.Persistence;
using YouDj.Infrastructure.Persistence.Repositories;
using YouDj.Infrastructure.Persistences.Repositories;
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

        services.Configure<JwtOptions>(configuration.GetSection("Jwt"));

        services.AddScoped<IQueueRepository, QueueRepository>();
        services.AddScoped<IDjRepository, DjRepository>();
        services.AddScoped<IPlaylistRepository, PlaylistRepository>();
        services.AddScoped<IGuestRepository, GuestRepository>();
        services.AddScoped<IPixPaymentRepository, PixPaymentRepository>();
        services.AddScoped<ISongOrderRepository, SongOrderRepository>();
        services.AddScoped<INowPlayingRepository, NowPlayingRepository>();

        services.AddScoped<IGuestSessionReader, GuestSessionReader>();
        services.AddScoped<IPlaylistSessionReader, PlaylistSessionReader>();

        services.AddScoped<IPasswordService, PasswordService>();
        services.AddScoped<IJwtTokenService, JwtTokenService>();
        services.AddScoped<IGuestTokenService, GuestTokenService>();
        services.AddScoped<IPlaylistSessionTokenService, PlaylistSessionTokenService>();
        services.AddScoped<IDjIdentityService, DjIdentityService>();
        services.AddScoped<IGuestTokenService, GuestTokenService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IJwtCookieFactory, JwtCookieFactory>();

        services.AddScoped<IDbErrorTranslator, PostgresDbErrorTranslator>();

        services.AddHttpClient<IYoutubeApiClient, YoutubeApiClient>();

        return services;
    }
}
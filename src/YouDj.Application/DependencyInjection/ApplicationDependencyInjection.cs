using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using YouDj.Application.Features.Queue.AddMusic;

namespace YouDj.Application.DependencyInjection;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());

        services.AddScoped<AddMusicUseCase>();

        return services;
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace YouDj.Infrastructure.Persistence;

public sealed class YouDjDbContextFactory
    : IDesignTimeDbContextFactory<YouDjDbContext>
{
    public YouDjDbContext CreateDbContext(string[] args)
    {
        var environment =
            Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
            ?? "Development";

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile($"appsettings.{environment}.json", optional: true)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<YouDjDbContext>();

        optionsBuilder.UseNpgsql(
            configuration.GetConnectionString("DefaultConnection"));

        return new YouDjDbContext(optionsBuilder.Options);
    }
}
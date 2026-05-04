using GameStore.Api.Application.Services.Genres;
using GameStore.Api.Application.Services.Games;
using GameStore.Api.Application.UseCases.Games;
using GameStore.Api.Application.UseCases.Genres;

namespace GameStore.Api.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var dbSection = configuration.GetSection("ConnectionStrings");
        var connectionString = dbSection["GameStore"];
        var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

        Console.WriteLine($"[Config] Loading configuration for Environment: {env}");

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException($"Connection string 'GameStore' is missing for environment '{env}'. Check appsettings.json or environment-specific config files.");
        }

        services.Configure<ApplicationConfiguration>(configuration);
        services.Configure<DatabaseConfiguration>(dbSection);

        return services;
    }

    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
    {
        services.AddScoped<CreateGameUseCase>();
        services.AddScoped<UpdateGameUseCase>();
        services.AddScoped<GetGameByIdUseCase>();
        services.AddScoped<ListGamesUseCase>();
        services.AddScoped<DeleteGameUseCase>();
        services.AddScoped<GetGenresUseCase>();

        services.AddScoped<IGameApplicationService, GameApplicationService>();
        services.AddScoped<IGenreApplicationService, GenreApplicationService>();

        return services;
    }
}

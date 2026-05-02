using GameStore.Api.Application.Services.Genres;
using GameStore.Api.Application.Services.Games;
using GameStore.Api.Application.UseCases.Games;
using GameStore.Api.Application.UseCases.Genres;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GameStore.Api.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ApplicationConfiguration>(configuration);
        services.Configure<DatabaseConfiguration>(configuration.GetSection("ConnectionStrings"));
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

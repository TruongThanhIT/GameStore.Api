using GameStore.Api.Domain;
using GameStore.Api.Domain.Events;
using GameStore.Api.Domain.Repositories;
using GameStore.Api.Infrastructure;
using GameStore.Api.Infrastructure.Events;
using GameStore.Api.Infrastructure.Repositories;
using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

public static class DataExtensions
{
    public static async Task MigrateDbAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();

        await dbContext.Database.MigrateAsync();
    }

    public static IServiceCollection AddGameStoreDb(this IServiceCollection services, IConfiguration configuration)
    {
        var connString = configuration.GetConnectionString("GameStore")
            ?? throw new InvalidOperationException("Connection string 'GameStore' not found.");

        if (connString.Contains(".db"))
        {
            services.AddSqlite<GameStoreContext>(
                connString,
                optionsAction: options => ConfigureCommonOptions(options)
            );
        }
        else
        {
            services.AddSqlServer<GameStoreContext>(
                connString,
                optionsAction: options => ConfigureCommonOptions(options)
            );
        }

        services.AddScoped<IGameRepository, GameRepository>();
        services.AddScoped<IGenreRepository, GenreRepository>();
        services.AddScoped<IDomainEventPublisher, DomainEventPublisher>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }

    private static void ConfigureCommonOptions(DbContextOptionsBuilder options)
    {
        options.UseAsyncSeeding(async (context, _, cancellationToken) =>
        {
            if (context is GameStoreContext gameContext)
            {
                await DbSeeder.SeedDataAsync(gameContext);
            }
        });

        options.UseSeeding((context, _) =>
        {
                
        });
        options.UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
    }
}
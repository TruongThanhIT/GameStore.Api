using Bogus;
using GameStore.Api.Data;
using GameStore.Api.Models;
using MyGameStore.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

public static class DbSeeder
{
    public static async Task SeedDataAsync(GameStoreContext context)
    {
        if (!await context.Genres.AnyAsync())
        {
            await context.Genres.AddRangeAsync(GetPredefinedGenres());
            await context.SaveChangesAsync();
        }

        if (!await context.Games.AnyAsync())
        {
            var genres = await context.Genres.ToListAsync();

            var gameFaker = new Faker<Game>()
                .RuleFor(g => g.Title, f => new GameTitle(f.Commerce.ProductName()))
                .RuleFor(g => g.Price, f => new GamePrice(Math.Round(f.Finance.Amount(5, 100), 2)))
                .RuleFor(g => g.GenreId, f => f.PickRandom(genres).Id)
                .RuleFor(g => g.ReleaseDate, f => new ReleaseDate(DateOnly.FromDateTime(f.Date.Past(10))));

            await context.Games.AddRangeAsync(gameFaker.Generate(50));
            await context.SaveChangesAsync();
        }
    }

    private static Genre[] GetPredefinedGenres() => [
        new Genre { Name = "Fighting" },
        new Genre { Name = "Action" },
        new Genre { Name = "Adventure" },
        new Genre { Name = "RPG" },
        new Genre { Name = "Strategy" },
        new Genre { Name = "Sports" },
        new Genre { Name = "Racing" }
    ];
}
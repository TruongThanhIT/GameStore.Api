using GameStore.Api.Data;
using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;

public static class DbSeeder
{
    public static void SeedData(GameStoreContext context)
    {
        if (!context.Genres.Any())
        {
            context.Genres.AddRange(GetPredefinedGenres());
            context.SaveChanges();
        }
    }

    public static async Task SeedDataAsync(GameStoreContext context)
    {
        if (!await context.Genres.AnyAsync())
        {
            await context.Genres.AddRangeAsync(GetPredefinedGenres());
            await context.SaveChangesAsync();
        }
    }

    private static Genre[] GetPredefinedGenres() => [
        new Genre { Name = "Fighting" },
        new Genre { Name = "Action" },
        new Genre { Name = "Adventure" },
        new Genre { Name = "RPG" },
        new Genre { Name = "Strategy" }
    ];
}
using GameStore.Api.Application.Services.Genres;

namespace GameStore.Api.Endpoints;

public static class GenresEndpoints
{
    public static void MapGenresEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/genres");

        // GET /genres
        group.MapGet("/", async (IGenreApplicationService genreService) =>
        {
            var result = await genreService.GetGenresAsync();
            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.Problem(result.Error);
        });
    }
}

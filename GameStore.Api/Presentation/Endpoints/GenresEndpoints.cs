using GameStore.Api.Application.Mappings;
using GameStore.Api.Application.UseCases.Genres;
using GameStore.Api.Data;
using GameStore.Api.Domain.Repositories;
using GameStore.Api.Dtos;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Endpoints;

public static class GenresEndpoints
{
    public static void MapGenresEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/genres");

        // GET /genres
        group.MapGet("/", async (GetGenresUseCase getGenresUseCase) =>
        {
            return await getGenresUseCase.ExecuteAsync();
        });
    }
}

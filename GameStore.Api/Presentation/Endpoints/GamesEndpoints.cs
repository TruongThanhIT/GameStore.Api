using GameStore.Api.Data;
using GameStore.Api.Domain.Exceptions;
using GameStore.Api.Domain.Repositories;
using GameStore.Api.Dtos;
using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;
using MyGameStore.Domain.ValueObjects;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetName";

    public static void MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/games");

        //GET /games
        group.MapGet("/", async (
            int? page,
            int? pageSize,
            GameStoreContext dbContext) =>
        {
            int currPage = page ?? 1;
            int size = pageSize ?? 10;
            return await dbContext.Games
                          .Include(game => game.Genre)
                          .Select(game => new GameSummaryDto(
                            game.Id,
                            game.Title.Value,
                            game.GenreId,
                            game.Genre!.Name,
                            game.Price,
                            game.ReleaseDate.Value
                          ))
                          .AsNoTracking()
                          .ToPagedListAsync(currPage, size);
        });

        // GET /games/1
        group.MapGet("/{id}", async (int id, IGameRepository gameRepository) =>
        {
            try
            {
                var game = await gameRepository.GetByIdAsync(id);
                return Results.Ok(
                    new GameDetailsDto(
                        game.Id,
                        game.Title.Value,
                        game.GenreId,
                        game.Price,
                        game.ReleaseDate.Value
                    )
                );
            }
            catch (GameNotFoundException)
            {
                return Results.NotFound();
            }
        })
        .WithName(GetGameEndpointName);

        // POST /games
        group.MapPost("/", async (CreateGameDto newGame, IGameRepository gameRepository) =>
        {
            Game game = new()
            {
                Title = new GameTitle(newGame.Name),
                GenreId = newGame.GenreId,
                Price = newGame.Price,
                ReleaseDate = new ReleaseDate(newGame.ReleaseDate)
            };

            await gameRepository.AddAsync(game);

            GameDetailsDto gameDto = new(
                game.Id,
                game.Title.Value,
                game.GenreId,
                game.Price,
                game.ReleaseDate.Value
            );

            return Results.CreatedAtRoute(GetGameEndpointName, new { id = gameDto.Id }, gameDto);
        });

        // PUT /games/1
        group.MapPut("/{id}", async (
            int id,
            UpdateGameDto updatedGame,
            IGameRepository gameRepository) =>
        {
            try
            {
                var existingGame = await gameRepository.GetByIdAsync(id);

                existingGame.Title = new GameTitle(updatedGame.Name);
                existingGame.GenreId = updatedGame.GenreId;
                existingGame.Price = updatedGame.Price;
                existingGame.ReleaseDate = new ReleaseDate(updatedGame.ReleaseDate);

                await gameRepository.UpdateAsync(existingGame);

                return Results.NoContent();
            }
            catch (GameNotFoundException)
            {
                return Results.NotFound();
            }
        });

        // DELETE /games/1
        group.MapDelete("/{id}", async (int id, IGameRepository gameRepository) =>
        {
            try
            {
                await gameRepository.DeleteAsync(id);
                return Results.NoContent();
            }
            catch (GameNotFoundException)
            {
                return Results.NotFound();
            }
        });
    }
}

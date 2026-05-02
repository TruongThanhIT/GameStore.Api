using GameStore.Api.Application.Mappings;
using GameStore.Api.Application.Services.Games;
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
            IGameApplicationService gameService) =>
        {
            int currPage = page ?? 1;
            int size = pageSize ?? 10;
            return await gameService.ListGamesAsync(currPage, size);
        });

        // GET /games/1
        group.MapGet("/{id}", async (int id, IGameApplicationService gameService) =>
        {
            try
            {
                var gameDto = await gameService.GetGameByIdAsync(id);
                return Results.Ok(gameDto);
            }
            catch (GameNotFoundException)
            {
                return Results.NotFound();
            }
        })
        .WithName(GetGameEndpointName);

        // POST /games
        group.MapPost("/", async (CreateGameDto newGame, IGameApplicationService gameService) =>
        {
            var gameDto = await gameService.CreateGameAsync(newGame);
            return Results.CreatedAtRoute(GetGameEndpointName, new { id = gameDto.Id }, gameDto);
        });

        // PUT /games/1
        group.MapPut("/{id}", async (
            int id,
            UpdateGameDto updatedGame,
            IGameApplicationService gameService) =>
        {
            try
            {
                await gameService.UpdateGameAsync(id, updatedGame);
                return Results.NoContent();
            }
            catch (GameNotFoundException)
            {
                return Results.NotFound();
            }
        });

        // DELETE /games/1
        group.MapDelete("/{id}", async (int id, IGameApplicationService gameService) =>
        {
            try
            {
                await gameService.DeleteGameAsync(id);
                return Results.NoContent();
            }
            catch (GameNotFoundException)
            {
                return Results.NotFound();
            }
        });
    }
}

using GameStore.Api.Application.Common;
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
            var result = await gameService.GetGameByIdAsync(id);
            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.NotFound(result.Error);
        })
        .WithName(GetGameEndpointName);

        // POST /games
        group.MapPost("/", async (CreateGameDto newGame, IGameApplicationService gameService) =>
        {
            var result = await gameService.CreateGameAsync(newGame);
            return result.IsSuccess
                ? Results.CreatedAtRoute(GetGameEndpointName, new { id = result.Value.Id }, result.Value)
                : Results.BadRequest(result.Error);
        });

        // PUT /games/1
        group.MapPut("/{id}", async (
            int id,
            UpdateGameDto updatedGame,
            IGameApplicationService gameService) =>
        {
            var result = await gameService.UpdateGameAsync(id, updatedGame);
            return result.IsSuccess
                ? Results.NoContent()
                : Results.NotFound(result.Error);
        });

        // DELETE /games/1
        group.MapDelete("/{id}", async (int id, IGameApplicationService gameService) =>
        {
            var result = await gameService.DeleteGameAsync(id);
            return result.IsSuccess
                ? Results.NoContent()
                : Results.NotFound(result.Error);
        });
    }
}

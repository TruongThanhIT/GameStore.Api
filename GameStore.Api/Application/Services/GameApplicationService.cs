using GameStore.Api.Application.UseCases.Games;
using GameStore.Api.Dtos;
using GameStore.Api.Models;

namespace GameStore.Api.Application.Services.Games;

public class GameApplicationService(
    ListGamesUseCase listGamesUseCase,
    GetGameByIdUseCase getGameByIdUseCase,
    CreateGameUseCase createGameUseCase,
    UpdateGameUseCase updateGameUseCase,
    DeleteGameUseCase deleteGameUseCase)
    : IGameApplicationService
{
    public Task<PagedList<GameSummaryDto>> ListGamesAsync(int pageNumber, int pageSize)
        => listGamesUseCase.ExecuteAsync(pageNumber, pageSize);

    public Task<GameDetailsDto> GetGameByIdAsync(int id)
        => getGameByIdUseCase.ExecuteAsync(id);

    public Task<GameDetailsDto> CreateGameAsync(CreateGameDto dto)
        => createGameUseCase.ExecuteAsync(dto);

    public Task UpdateGameAsync(int id, UpdateGameDto dto)
        => updateGameUseCase.ExecuteAsync(id, dto);

    public Task DeleteGameAsync(int id)
        => deleteGameUseCase.ExecuteAsync(id);
}

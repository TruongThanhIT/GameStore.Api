using GameStore.Api.Application.Common;
using GameStore.Api.Application.UseCases.Games;
using GameStore.Api.Domain.Exceptions;
using GameStore.Api.Dtos;

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

    public async Task<Result<GameDetailsDto>> GetGameByIdAsync(int id)
    {
        try
        {
            var gameDto = await getGameByIdUseCase.ExecuteAsync(id);
            return Result<GameDetailsDto>.Success(gameDto);
        }
        catch (GameNotFoundException ex)
        {
            return Result<GameDetailsDto>.Failure(ex.Message);
        }
    }

    public async Task<Result<GameDetailsDto>> CreateGameAsync(CreateGameDto dto)
    {
        try
        {
            var gameDto = await createGameUseCase.ExecuteAsync(dto);
            return Result<GameDetailsDto>.Success(gameDto);
        }
        catch (Exception ex)
        {
            return Result<GameDetailsDto>.Failure(ex.Message);
        }
    }

    public async Task<Result> UpdateGameAsync(int id, UpdateGameDto dto)
    {
        try
        {
            await updateGameUseCase.ExecuteAsync(id, dto);
            return Result.Success();
        }
        catch (GameNotFoundException ex)
        {
            return Result.Failure(ex.Message);
        }
    }

    public async Task<Result> DeleteGameAsync(int id)
    {
        try
        {
            await deleteGameUseCase.ExecuteAsync(id);
            return Result.Success();
        }
        catch (GameNotFoundException ex)
        {
            return Result.Failure(ex.Message);
        }
    }
}

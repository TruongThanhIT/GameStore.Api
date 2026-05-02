using GameStore.Api.Application.Common;
using GameStore.Api.Dtos;
using GameStore.Api.Models;

namespace GameStore.Api.Application.Services.Games;

public interface IGameApplicationService
{
    Task<PagedList<GameSummaryDto>> ListGamesAsync(int pageNumber, int pageSize);
    Task<Result<GameDetailsDto>> GetGameByIdAsync(int id);
    Task<Result<GameDetailsDto>> CreateGameAsync(CreateGameDto dto);
    Task<Result> UpdateGameAsync(int id, UpdateGameDto dto);
    Task<Result> DeleteGameAsync(int id);
}

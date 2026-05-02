using GameStore.Api.Dtos;
using GameStore.Api.Models;

namespace GameStore.Api.Application.Services.Games;

public interface IGameApplicationService
{
    Task<PagedList<GameSummaryDto>> ListGamesAsync(int pageNumber, int pageSize);
    Task<GameDetailsDto> GetGameByIdAsync(int id);
    Task<GameDetailsDto> CreateGameAsync(CreateGameDto dto);
    Task UpdateGameAsync(int id, UpdateGameDto dto);
    Task DeleteGameAsync(int id);
}

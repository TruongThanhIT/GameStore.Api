using GameStore.Api.Application.Mappings;
using GameStore.Api.Domain.Repositories;
using GameStore.Api.Dtos;

namespace GameStore.Api.Application.UseCases.Games;

public class UpdateGameUseCase(IGameRepository gameRepository)
{
    public async Task ExecuteAsync(int id, UpdateGameDto dto)
    {
        var game = await gameRepository.GetByIdAsync(id);
        dto.UpdateGame(game);
        await gameRepository.UpdateAsync(game);
    }
}
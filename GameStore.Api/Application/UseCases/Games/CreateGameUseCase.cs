using GameStore.Api.Application.Mappings;
using GameStore.Api.Domain.Repositories;
using GameStore.Api.Dtos;

namespace GameStore.Api.Application.UseCases.Games;

public class CreateGameUseCase(IGameRepository gameRepository)
{
    public async Task<GameDetailsDto> ExecuteAsync(CreateGameDto dto)
    {
        var game = dto.ToGame();
        await gameRepository.AddAsync(game);
        return game.ToGameDetailsDto();
    }
}
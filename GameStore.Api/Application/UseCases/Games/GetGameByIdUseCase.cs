using GameStore.Api.Application.Mappings;
using GameStore.Api.Domain.Exceptions;
using GameStore.Api.Domain.Repositories;
using GameStore.Api.Dtos;

namespace GameStore.Api.Application.UseCases.Games;

public class GetGameByIdUseCase(IGameRepository gameRepository)
{
    public async Task<GameDetailsDto> ExecuteAsync(int id)
    {
        var game = await gameRepository.GetByIdAsync(id);
        return game.ToGameDetailsDto();
    }
}
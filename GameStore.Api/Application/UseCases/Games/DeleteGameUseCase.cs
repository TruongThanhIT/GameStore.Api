using GameStore.Api.Domain.Exceptions;
using GameStore.Api.Domain.Repositories;

namespace GameStore.Api.Application.UseCases.Games;

public class DeleteGameUseCase(IGameRepository gameRepository)
{
    public async Task ExecuteAsync(int id)
    {
        await gameRepository.DeleteAsync(id);
    }
}
using GameStore.Api.Models;

namespace GameStore.Api.Domain.Repositories;

public interface IGameRepository
{
    Task<Game> GetByIdAsync(int id);
    Task<IEnumerable<Game>> GetAllAsync();
    Task AddAsync(Game game);
    Task UpdateAsync(Game game);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}
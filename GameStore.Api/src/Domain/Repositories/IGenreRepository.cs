using GameStore.Api.Models;

namespace GameStore.Api.Domain.Repositories;

public interface IGenreRepository
{
    Task<Genre> GetByIdAsync(int id);
    Task<IEnumerable<Genre>> GetAllAsync();
    Task AddAsync(Genre genre);
    Task UpdateAsync(Genre genre);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}
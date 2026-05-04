using GameStore.Api.Data;
using GameStore.Api.Domain;
using GameStore.Api.Domain.Exceptions;
using GameStore.Api.Domain.Repositories;
using GameStore.Api.Models;

namespace GameStore.Api.Infrastructure.Repositories;

public class GenreRepository(GameStoreContext context, IUnitOfWork unitOfWork) : BaseRepository<Genre>(context, unitOfWork), IGenreRepository
{
    public override async Task<Genre> GetByIdAsync(int id)
    {
        var genre = await _dbSet.FindAsync(id);
        if (genre == null)
        {
            throw new DomainException($"Genre with ID {id} was not found.");
        }
        return genre;
    }

    public override async Task UpdateAsync(Genre entity)
    {
        var existing = await _dbSet.FindAsync(entity.Id);
        if (existing == null)
        {
            throw new DomainException($"Genre with ID {entity.Id} was not found.");
        }
        await base.UpdateAsync(entity);
    }

    public override async Task DeleteAsync(int id)
    {
        var existing = await _dbSet.FindAsync(id);
        if (existing == null)
        {
            throw new DomainException($"Genre with ID {id} was not found.");
        }
        await base.DeleteAsync(id);
    }

    // Additional genre-specific repository methods can be added here
}
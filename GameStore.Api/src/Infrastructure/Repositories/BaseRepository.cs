using GameStore.Api.Data;
using GameStore.Api.Domain;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Infrastructure.Repositories;

public abstract class BaseRepository<T>(GameStoreContext context, IUnitOfWork unitOfWork) where T : class
{
    protected readonly GameStoreContext _context = context;
    protected readonly IUnitOfWork _unitOfWork = unitOfWork;
    protected readonly DbSet<T> _dbSet = context.Set<T>();

    public virtual async Task<T> GetByIdAsync(int id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity == null)
        {
            throw new InvalidOperationException($"Entity with ID {id} was not found.");
        }
        return entity;
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public virtual async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync();
    }

    public virtual async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _unitOfWork.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _unitOfWork.SaveChangesAsync();
        }
    }

    public virtual async Task<bool> ExistsAsync(int id)
    {
        return await _dbSet.FindAsync(id) != null;
    }
}
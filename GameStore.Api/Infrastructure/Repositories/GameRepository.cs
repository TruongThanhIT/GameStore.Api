using GameStore.Api.Data;
using GameStore.Api.Domain;
using GameStore.Api.Domain.Events;
using GameStore.Api.Domain.Exceptions;
using GameStore.Api.Domain.Repositories;
using GameStore.Api.Models;

namespace GameStore.Api.Infrastructure.Repositories;

public class GameRepository(GameStoreContext context, IUnitOfWork unitOfWork, IDomainEventPublisher eventPublisher) : BaseRepository<Game>(context, unitOfWork), IGameRepository
{
    private readonly IDomainEventPublisher _eventPublisher = eventPublisher;

    public override async Task<Game> GetByIdAsync(int id)
    {
        var game = await _dbSet.FindAsync(id);
        if (game == null)
        {
            throw new GameNotFoundException(id);
        }
        return game;
    }

    public override async Task UpdateAsync(Game entity)
    {
        var existing = await _dbSet.FindAsync(entity.Id);
        if (existing == null)
        {
            throw new GameNotFoundException(entity.Id);
        }
        await base.UpdateAsync(entity);
    }

    public override async Task DeleteAsync(int id)
    {
        var existing = await _dbSet.FindAsync(id);
        if (existing == null)
        {
            throw new GameNotFoundException(id);
        }
        await base.DeleteAsync(id);
        await _eventPublisher.PublishAsync(new GameDeletedEvent(id));
    }

    public override async Task AddAsync(Game entity)
    {
        await base.AddAsync(entity);
        await _eventPublisher.PublishAsync(new GameCreatedEvent(entity));
    }
}
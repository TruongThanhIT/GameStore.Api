using GameStore.Api.Data;
using GameStore.Api.Domain;

namespace GameStore.Api.Infrastructure;

public class UnitOfWork(GameStoreContext context) : IUnitOfWork
{
    private readonly GameStoreContext _context = context;

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}
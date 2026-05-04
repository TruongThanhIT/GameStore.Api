using GameStore.Api.Application.Mappings;
using GameStore.Api.Data;
using GameStore.Api.Dtos;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Application.UseCases.Games;

public class ListGamesUseCase(GameStoreContext dbContext)
{
    public async Task<PagedList<GameSummaryDto>> ExecuteAsync(int pageNumber, int pageSize)
    {
        return await dbContext.Games
            .Include(g => g.Genre)
            .Select(g => g.ToGameSummaryDto())
            .AsNoTracking()
            .ToPagedListAsync(pageNumber, pageSize);
    }
}
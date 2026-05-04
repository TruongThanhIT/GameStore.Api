using GameStore.Api.Application.Common;
using GameStore.Api.Application.UseCases.Genres;
using GameStore.Api.Dtos;

namespace GameStore.Api.Application.Services.Genres;

public class GenreApplicationService(GetGenresUseCase getGenresUseCase) : IGenreApplicationService
{
    public async Task<Result<IEnumerable<GenreDto>>> GetGenresAsync()
    {
        try
        {
            var genres = await getGenresUseCase.ExecuteAsync();
            return Result<IEnumerable<GenreDto>>.Success(genres);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<GenreDto>>.Failure(ex.Message);
        }
    }
}
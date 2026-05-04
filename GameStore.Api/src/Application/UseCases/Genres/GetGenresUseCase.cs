using GameStore.Api.Application.Mappings;
using GameStore.Api.Domain.Repositories;
using GameStore.Api.Dtos;

namespace GameStore.Api.Application.UseCases.Genres;

public class GetGenresUseCase(IGenreRepository genreRepository)
{
    public async Task<IEnumerable<GenreDto>> ExecuteAsync()
    {
        var genres = await genreRepository.GetAllAsync();
        return genres.Select(g => g.ToGenreDto());
    }
}
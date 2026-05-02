using GameStore.Api.Application.Common;
using GameStore.Api.Application.UseCases.Genres;
using GameStore.Api.Dtos;

namespace GameStore.Api.Application.Services.Genres;

public interface IGenreApplicationService
{
    Task<Result<IEnumerable<GenreDto>>> GetGenresAsync();
}
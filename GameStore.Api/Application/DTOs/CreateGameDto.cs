using System.ComponentModel.DataAnnotations;
using MyGameStore.Domain.ValueObjects;

namespace GameStore.Api.Dtos;

public record CreateGameDto(
    [Required][StringLength(50)] string Name,
    [Range(1,50)] int GenreId,
    GamePrice Price,
    DateOnly ReleaseDate
);


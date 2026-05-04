using MyGameStore.Domain.ValueObjects;

namespace GameStore.Api.Dtos;

public record UpdateGameDto(
    string Name,
    int GenreId,
    GamePrice Price,
    DateOnly ReleaseDate
);

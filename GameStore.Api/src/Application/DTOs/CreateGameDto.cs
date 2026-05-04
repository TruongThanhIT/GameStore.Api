using MyGameStore.Domain.ValueObjects;

namespace GameStore.Api.Dtos;

public record CreateGameDto(
    string Name,
    int GenreId,
    GamePrice Price,
    DateOnly ReleaseDate
);


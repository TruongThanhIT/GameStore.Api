using MyGameStore.Domain.ValueObjects;

namespace GameStore.Api.Dtos;

public record GameDetailsDto(
    int Id,
    string Name,
    int GenreId,
    GamePrice Price,
    DateOnly ReleaseDate
);

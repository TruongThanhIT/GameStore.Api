using MyGameStore.Domain.ValueObjects;

namespace GameStore.Api.Dtos;

public record GameSummaryDto(
    int Id,
    string Name,
    int GenreId,
    string GenreName,
    GamePrice Price,
    DateOnly ReleaseDate
);

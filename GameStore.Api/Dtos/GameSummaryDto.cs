namespace GameStore.Api.Dtos;

public record GameSummaryDto(
    int Id,
    string Name,
    int GenreId,
    string GenreName,
    decimal Price,
    DateOnly ReleaseDate
);

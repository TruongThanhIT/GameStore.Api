using GameStore.Api.Dtos;
using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;
using MyGameStore.Domain.ValueObjects;

namespace GameStore.Api.Application.Mappings;

public static class MappingExtensions
{
    public static async Task<PagedList<T>> ToPagedListAsync<T>(
        this IQueryable<T> source,
        int pageNumber,
        int pageSize)
    {
        var count = await source.CountAsync();
        var items = await source.Skip((pageNumber - 1) * pageSize)
                                .Take(pageSize)
                                .ToListAsync();

        return new PagedList<T>(items, count, pageNumber, pageSize);
    }

    public static GameSummaryDto ToGameSummaryDto(this Game game) =>
        new GameSummaryDto(
            game.Id,
            game.Title.Value,
            game.GenreId,
            game.Genre?.Name ?? "Unknown",
            game.Price,
            game.ReleaseDate.Value
        );

    public static GameDetailsDto ToGameDetailsDto(this Game game) =>
        new GameDetailsDto(
            game.Id,
            game.Title.Value,
            game.GenreId,
            game.Price,
            game.ReleaseDate.Value
        );

    public static GenreDto ToGenreDto(this Genre genre) =>
        new GenreDto(genre.Id, genre.Name);

    public static Game ToGame(this CreateGameDto dto) =>
        new Game
        {
            Title = new GameTitle(dto.Name),
            GenreId = dto.GenreId,
            Price = dto.Price,
            ReleaseDate = new ReleaseDate(dto.ReleaseDate)
        };

    public static void UpdateGame(this UpdateGameDto dto, Game game)
    {
        game.Title = new GameTitle(dto.Name);
        game.GenreId = dto.GenreId;
        game.Price = dto.Price;
        game.ReleaseDate = new ReleaseDate(dto.ReleaseDate);
    }
}
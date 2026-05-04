using FluentAssertions;
using GameStore.Api.Application.UseCases.Games;
using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;
using MyGameStore.Domain.ValueObjects;

namespace GameStore.Api.Test.UseCases.Games;

public class ListGamesUseCaseTests
{
    private readonly GameStoreContext _dbContext;
    private readonly ListGamesUseCase _useCase;

    public ListGamesUseCaseTests()
    {
        var options = new DbContextOptionsBuilder<GameStoreContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _dbContext = new GameStoreContext(options);
        _useCase = new ListGamesUseCase(_dbContext);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnPagedListOfGameSummaryDto()
    {
        // Arrange
        var games = new List<Game>
        {
            new Game
            {
                Id = 1,
                Title = new GameTitle("Game 1"),
                GenreId = 1,
                Genre = new Genre { Id = 1, Name = "Action" },
                Price = new GamePrice(29.99m),
                ReleaseDate = new ReleaseDate(new DateOnly(2023, 1, 1))
            },
            new Game
            {
                Id = 2,
                Title = new GameTitle("Game 2"),
                GenreId = 2,
                Genre = new Genre { Id = 2, Name = "RPG" },
                Price = new GamePrice(49.99m),
                ReleaseDate = new ReleaseDate(new DateOnly(2023, 6, 15))
            }
        };

        _dbContext.Games.AddRange(games);
        await _dbContext.SaveChangesAsync();

        // Act
        var result = await _useCase.ExecuteAsync(1, 10);

        // Assert
        result.Should().NotBeNull();
        result.Items.Should().HaveCount(2);
        result.TotalCount.Should().Be(2);
        result.PageNumber.Should().Be(1);

        var firstGame = result.Items.First();
        firstGame.Id.Should().Be(1);
        firstGame.Name.Should().Be("Game 1");
        firstGame.GenreId.Should().Be(1);
        firstGame.GenreName.Should().Be("Action");
        firstGame.Price.Amount.Should().Be(29.99m);
        firstGame.ReleaseDate.Should().Be(new DateOnly(2023, 1, 1));
    }
}
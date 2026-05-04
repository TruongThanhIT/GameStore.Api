using FluentAssertions;
using GameStore.Api.Application.UseCases.Games;
using GameStore.Api.Domain.Repositories;
using GameStore.Api.Dtos;
using GameStore.Api.Models;
using MyGameStore.Domain.ValueObjects;
using NSubstitute;

namespace GameStore.Api.Test.UseCases.Games;

public class UpdateGameUseCaseTests
{
    private readonly IGameRepository _gameRepository = Substitute.For<IGameRepository>();
    private readonly UpdateGameUseCase _useCase;

    public UpdateGameUseCaseTests()
    {
        _useCase = new UpdateGameUseCase(_gameRepository);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldUpdateGameAndCallRepositoryMethods()
    {
        // Arrange
        var gameId = 1;
        var existingGame = new Game
        {
            Id = gameId,
            Title = new GameTitle("Old Game"),
            GenreId = 1,
            Price = new GamePrice(19.99m),
            ReleaseDate = new ReleaseDate(new DateOnly(2022, 1, 1))
        };

        var updateGameDto = new UpdateGameDto(
            "Updated Game",
            2,
            new GamePrice(39.99m),
            new DateOnly(2024, 6, 15)
        );

        _gameRepository.GetByIdAsync(gameId).Returns(existingGame);
        _gameRepository.UpdateAsync(existingGame).Returns(Task.CompletedTask);

        // Act
        await _useCase.ExecuteAsync(gameId, updateGameDto);

        // Assert
        await _gameRepository.Received(1).GetByIdAsync(gameId);
        await _gameRepository.Received(1).UpdateAsync(Arg.Is<Game>(g =>
            g.Id == gameId &&
            g.Title.Value == "Updated Game" &&
            g.GenreId == 2 &&
            g.Price.Amount == 39.99m &&
            g.ReleaseDate.Value == new DateOnly(2024, 6, 15)));
    }
}
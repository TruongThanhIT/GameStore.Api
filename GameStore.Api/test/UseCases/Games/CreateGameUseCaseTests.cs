using FluentAssertions;
using GameStore.Api.Application.UseCases.Games;
using GameStore.Api.Domain.Repositories;
using GameStore.Api.Dtos;
using GameStore.Api.Models;
using MyGameStore.Domain.ValueObjects;
using NSubstitute;

namespace GameStore.Api.Test.UseCases.Games;

public class CreateGameUseCaseTests
{
    private readonly IGameRepository _gameRepository = Substitute.For<IGameRepository>();
    private readonly CreateGameUseCase _useCase;

    public CreateGameUseCaseTests()
    {
        _useCase = new CreateGameUseCase(_gameRepository);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldCreateGameAndReturnGameDetailsDto()
    {
        // Arrange
        var createGameDto = new CreateGameDto(
            "Test Game",
            1,
            new GamePrice(29.99m),
            new DateOnly(2023, 1, 1)
        );

        var expectedGame = new Game
        {
            Id = 1,
            Title = new GameTitle("Test Game"),
            GenreId = 1,
            Price = new GamePrice(29.99m),
            ReleaseDate = new ReleaseDate(new DateOnly(2023, 1, 1))
        };

        _gameRepository.AddAsync(Arg.Any<Game>()).Returns(callInfo =>
        {
            var game = callInfo.Arg<Game>();
            game.Id = expectedGame.Id;
            return Task.CompletedTask;
        });

        // Act
        var result = await _useCase.ExecuteAsync(createGameDto);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(expectedGame.Id);
        result.Name.Should().Be("Test Game");
        result.GenreId.Should().Be(1);
        result.Price.Amount.Should().Be(29.99m);
        result.ReleaseDate.Should().Be(new DateOnly(2023, 1, 1));

        await _gameRepository.Received(1).AddAsync(Arg.Is<Game>(g =>
            g.Title.Value == "Test Game" &&
            g.GenreId == 1 &&
            g.Price.Amount == 29.99m &&
            g.ReleaseDate.Value == new DateOnly(2023, 1, 1)));
    }
}
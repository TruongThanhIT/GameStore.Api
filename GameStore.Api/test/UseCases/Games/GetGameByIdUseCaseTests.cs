using FluentAssertions;
using GameStore.Api.Application.UseCases.Games;
using GameStore.Api.Domain.Repositories;
using GameStore.Api.Dtos;
using GameStore.Api.Models;
using MyGameStore.Domain.ValueObjects;
using NSubstitute;

namespace GameStore.Api.Test.UseCases.Games;

public class GetGameByIdUseCaseTests
{
    private readonly IGameRepository _gameRepository = Substitute.For<IGameRepository>();
    private readonly GetGameByIdUseCase _useCase;

    public GetGameByIdUseCaseTests()
    {
        _useCase = new GetGameByIdUseCase(_gameRepository);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnGameDetailsDto_WhenGameExists()
    {
        // Arrange
        var gameId = 1;
        var game = new Game
        {
            Id = gameId,
            Title = new GameTitle("Test Game"),
            GenreId = 1,
            Price = new GamePrice(29.99m),
            ReleaseDate = new ReleaseDate(new DateOnly(2023, 1, 1))
        };

        _gameRepository.GetByIdAsync(gameId).Returns(game);

        // Act
        var result = await _useCase.ExecuteAsync(gameId);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(gameId);
        result.Name.Should().Be("Test Game");
        result.GenreId.Should().Be(1);
        result.Price.Amount.Should().Be(29.99m);
        result.ReleaseDate.Should().Be(new DateOnly(2023, 1, 1));

        await _gameRepository.Received(1).GetByIdAsync(gameId);
    }
}
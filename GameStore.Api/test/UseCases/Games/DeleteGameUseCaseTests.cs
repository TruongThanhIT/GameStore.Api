using FluentAssertions;
using GameStore.Api.Application.UseCases.Games;
using GameStore.Api.Domain.Repositories;
using NSubstitute;

namespace GameStore.Api.Test.UseCases.Games;

public class DeleteGameUseCaseTests
{
    private readonly IGameRepository _gameRepository = Substitute.For<IGameRepository>();
    private readonly DeleteGameUseCase _useCase;

    public DeleteGameUseCaseTests()
    {
        _useCase = new DeleteGameUseCase(_gameRepository);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldCallRepositoryDeleteMethod()
    {
        // Arrange
        var gameId = 1;
        _gameRepository.DeleteAsync(gameId).Returns(Task.CompletedTask);

        // Act
        await _useCase.ExecuteAsync(gameId);

        // Assert
        await _gameRepository.Received(1).DeleteAsync(gameId);
    }
}
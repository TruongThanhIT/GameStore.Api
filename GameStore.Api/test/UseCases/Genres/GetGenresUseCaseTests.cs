using FluentAssertions;
using GameStore.Api.Application.UseCases.Genres;
using GameStore.Api.Domain.Repositories;
using GameStore.Api.Dtos;
using GameStore.Api.Models;
using NSubstitute;

namespace GameStore.Api.Test.UseCases.Genres;

public class GetGenresUseCaseTests
{
    private readonly IGenreRepository _genreRepository = Substitute.For<IGenreRepository>();
    private readonly GetGenresUseCase _useCase;

    public GetGenresUseCaseTests()
    {
        _useCase = new GetGenresUseCase(_genreRepository);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnAllGenresAsGenreDtos()
    {
        // Arrange
        var genres = new List<Genre>
        {
            new Genre { Id = 1, Name = "Action" },
            new Genre { Id = 2, Name = "RPG" },
            new Genre { Id = 3, Name = "Strategy" }
        };

        _genreRepository.GetAllAsync().Returns(genres);

        // Act
        var result = await _useCase.ExecuteAsync();

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(3);

        var genreList = result.ToList();
        genreList[0].Id.Should().Be(1);
        genreList[0].Name.Should().Be("Action");
        genreList[1].Id.Should().Be(2);
        genreList[1].Name.Should().Be("RPG");
        genreList[2].Id.Should().Be(3);
        genreList[2].Name.Should().Be("Strategy");

        await _genreRepository.Received(1).GetAllAsync();
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnEmptyCollection_WhenNoGenresExist()
    {
        // Arrange
        var genres = new List<Genre>();
        _genreRepository.GetAllAsync().Returns(genres);

        // Act
        var result = await _useCase.ExecuteAsync();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEmpty();

        await _genreRepository.Received(1).GetAllAsync();
    }
}
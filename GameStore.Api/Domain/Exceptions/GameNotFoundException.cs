namespace GameStore.Api.Domain.Exceptions;

public class GameNotFoundException : DomainException
{
    public int GameId { get; }

    public GameNotFoundException(int gameId)
        : base($"Game with ID {gameId} was not found.")
    {
        GameId = gameId;
    }

    public GameNotFoundException(int gameId, string message)
        : base(message)
    {
        GameId = gameId;
    }
}
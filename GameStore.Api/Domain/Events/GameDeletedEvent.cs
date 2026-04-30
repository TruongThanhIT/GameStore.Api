namespace GameStore.Api.Domain.Events;

public class GameDeletedEvent : DomainEvent
{
    public int GameId { get; }

    public GameDeletedEvent(int gameId)
    {
        GameId = gameId;
    }
}
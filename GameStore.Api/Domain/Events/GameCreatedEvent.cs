using GameStore.Api.Models;

namespace GameStore.Api.Domain.Events;

public class GameCreatedEvent : DomainEvent
{
    public Game Game { get; }

    public GameCreatedEvent(Game game)
    {
        Game = game;
    }
}
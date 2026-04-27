using MyGameStore.Domain.ValueObjects;

namespace GameStore.Api.Models;

public class Game
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public Genre? Genre { get; set; }

    public int GenreId { get; set; }

    public required GamePrice Price { get; set; }

    public DateOnly ReleaseDate { get; set; }

    public bool IsAffordable(decimal userBalance) => userBalance >= Price.Amount;

    public bool IsDiscounted(decimal originalPrice) => Price.Amount < originalPrice;
}

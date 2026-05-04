namespace MyGameStore.Domain.ValueObjects
{
    public record GamePrice
    {
        public decimal Amount { get; init; }

        public string Currency { get; init; } = "USD";

        public GamePrice(decimal amount, string currency = "USD")
        {
            if (amount < 0)
                throw new ArgumentException("Price amount cannot be negative.", nameof(amount));
            if (amount > 1000)
                throw new ArgumentException("Price amount cannot exceed 1000.", nameof(amount));
            if (string.IsNullOrEmpty(currency))
                throw new ArgumentException("Currency cannot be null or empty.", nameof(currency));
            Amount = amount;
            Currency = currency;
        }

        public bool IsFree => Amount == 0;

        public static GamePrice Free() => new GamePrice(0);

        public override string ToString() => $"{Amount} {Currency}";
    }
}
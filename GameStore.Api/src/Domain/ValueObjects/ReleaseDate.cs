namespace MyGameStore.Domain.ValueObjects
{
    public record ReleaseDate
    {
        public DateOnly Value { get; init; }

        public ReleaseDate(DateOnly value)
        {
            if (value == default)
                throw new ArgumentException("Release date cannot be the default value.", nameof(value));
            if (value > DateOnly.FromDateTime(DateTime.Now))
                throw new ArgumentException("Release date cannot be in the future.", nameof(value));
            Value = value;
        }

        public override string ToString() => Value.ToString("yyyy-MM-dd");
    }
}
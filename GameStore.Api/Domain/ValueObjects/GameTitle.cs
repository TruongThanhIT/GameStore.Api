using System;

namespace MyGameStore.Domain.ValueObjects
{
    public record GameTitle
    {
        public string Value { get; init; }

        public GameTitle(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Game title cannot be null, empty, or whitespace.", nameof(value));
            if (value.Length > 100)
                throw new ArgumentException("Game title cannot exceed 100 characters.", nameof(value));
            Value = value;
        }

        public override string ToString() => Value;
    }
}
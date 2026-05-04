namespace GameStore.Api.Configuration;

public sealed class ApplicationConfiguration
{
    public string[] AllowedOrigins { get; init; } = Array.Empty<string>();
}

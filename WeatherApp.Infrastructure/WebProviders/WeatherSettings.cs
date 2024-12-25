namespace WeatherApp.Infrastructure.WebProviders;

public class WeatherSettings
{
    public const string Section = "WeatherSettings";

    public string ApiKey { get; set; } = null!;
    public string BaseUrl { get; set; } = null!;
}
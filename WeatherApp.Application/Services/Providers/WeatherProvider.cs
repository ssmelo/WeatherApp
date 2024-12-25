namespace WeatherApp.Application.Services;

public interface WeatherProvider
{
    Task<WeatherProviderResponse?> GetWeather(WeatherProviderParams weatherProviderParams);
}
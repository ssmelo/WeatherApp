namespace WeatherApp.Contracts;

public record WeatherResponse(
    DateTime Date,
    double MinTemperature,
    double MaxTemperature,
    double MinHumidity,
    double MaxHumidity,
    double MinTemperatureFeel,
    double MaxTemperatureFeel,
    double Precipitation,
    double PrecipitationProbability)
{
}
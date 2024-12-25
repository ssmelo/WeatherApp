namespace WeatherApp.Domain.Entities;

public class WeatherEvent
{
    public DateTime Date { get; }
    public double MinTemperature { get; }
    public double MaxTemperature { get; }
    public double Humidity { get; }
    public double MinTemperatureFeel { get; }
    public double MaxTemperatureFeel { get; }
    public double Precipitation { get; }
    public double PrecipitationProbability { get; }

    public WeatherEvent(DateTime date, double minTemperature, double maxTemperature, double humidity, double minTemperatureFeel, double maxTemperatureFeel, double precipitation, double precipitationProbability)
    {
        Date = date;
        MinTemperature = minTemperature;
        MaxTemperature = maxTemperature;
        Humidity = humidity;
        MinTemperatureFeel = minTemperatureFeel;
        MaxTemperatureFeel = maxTemperatureFeel;
        Precipitation = precipitation;
        PrecipitationProbability = precipitationProbability;
    }
}
namespace WeatherApp.Domain.Entities;

public class WeatherHistory
{
    public List<WeatherEvent> WeatherEvents { get; private set; } = [];

    public WeatherHistory(List<WeatherEvent> weatherEvents)
    {
        WeatherEvents = weatherEvents;
    }
}
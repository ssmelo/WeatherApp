namespace WeatherApp.Domain.Entities;

public class WeatherRoot
{
    public String City { get; private set; }
    public String State { get; private set; }
    public WeatherHistory WeatherHistory { get; private set; }

    public WeatherRoot(string city, string state, WeatherHistory weatherHistory)
    {
        City = city;
        State = state;
        WeatherHistory = weatherHistory;
    }
}
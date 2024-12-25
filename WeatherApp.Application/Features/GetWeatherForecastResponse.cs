using WeatherApp.Application.Services;

namespace WeatherApp.Application.Features;

public record GetWeatherForecastResponse(List<Day> days);
namespace WeatherApp.Application.Services;

public record WeatherProviderResponse(List<Day> days);

public record Day(
    DateTime datetime,
    double tempmax,
    double tempmin,
    double feelslikemax,
    double feelslikemin,
    double humidity,
    double precip,
    double precipprob);

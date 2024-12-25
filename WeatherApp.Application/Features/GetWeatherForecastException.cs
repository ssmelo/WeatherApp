using WeatherApp.Application.Common;

namespace WeatherApp.Application.Features;

public class GetWeatherForecastException : BaseApplicationException
{
    public GetWeatherForecastException(string message, ErrorType errorType) : base(message, errorType)
    {
        
    }
}
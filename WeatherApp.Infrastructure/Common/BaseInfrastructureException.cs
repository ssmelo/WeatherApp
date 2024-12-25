using WeatherApp.Application.Common;

namespace WeatherApp.Infrastructure.Common;

public class BaseInfrastructureException : Exception
{
    public BaseInfrastructureException(string message) : base(message)
    {
    }
}
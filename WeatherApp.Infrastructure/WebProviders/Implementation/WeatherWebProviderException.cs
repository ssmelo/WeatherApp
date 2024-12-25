using WeatherApp.Application.Common;
using WeatherApp.Infrastructure.Common;

namespace WeatherApp.Infrastructure.WebProviders;

public class WeatherWebProviderException : BaseInfrastructureException
{
    public WeatherWebProviderException(string message) : base(message)
    {
        
    }
}
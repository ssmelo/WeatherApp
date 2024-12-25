using Microsoft.Extensions.DependencyInjection;
using WeatherApp.Application.Features;

namespace WeatherApp.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<GetWeatherForecastUseCase>();
        
        return services;
    }
}
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using WeatherApp.Application.Services;

namespace WeatherApp.Infrastructure.WebProviders;

public static class WebProvidersExtension
{
    public static IServiceCollection AddWebProviders(this IServiceCollection services, IConfiguration configuration)
    {
        AddWeatherWebService(services, configuration);
        return services;
    }

    private static void AddWeatherWebService(this IServiceCollection services,
        IConfiguration configuration)
    {
        var weatherSettings = new WeatherSettings();
        configuration.Bind(WeatherSettings.Section, weatherSettings);
        services.AddSingleton(Options.Create(weatherSettings));
        
        services.AddTransient<WeatherWebProviderInterceptor>();

        services.AddHttpClient("weather", (serviceProvider, client) =>
            {
                var settings = serviceProvider
                    .GetRequiredService<IOptions<WeatherSettings>>().Value;

                client.BaseAddress = new Uri(settings.BaseUrl);
            })
            .AddHttpMessageHandler<WeatherWebProviderInterceptor>();
        
        services.AddTransient<WeatherWebProvider>();
        services.AddTransient<WeatherProvider>(serviceProvider => 
            new CachedWeatherProvider(
                serviceProvider.GetRequiredService<WeatherWebProvider>(),
                serviceProvider.GetRequiredService<IDistributedCache>()));
    }
}
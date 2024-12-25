using WeatherApp.Api.Middlewares;

namespace WeatherApp.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        
        return services;
    }

    public static IServiceCollection AddLoggingProvider(this IServiceCollection services)
    {
        services.AddLogging((loggingBuilder) => loggingBuilder
                .SetMinimumLevel(LogLevel.Debug)
                .AddConsole());
        
        return services;
    }

    public static IServiceCollection AddCacheProvider(this IServiceCollection services, IConfiguration configuration)
    {
        // services.AddDistributedMemoryCache(); // in memory cache
        services.AddStackExchangeRedisCache(options =>
            options.Configuration = configuration.GetConnectionString("Cache"));
        
        return services;
    }
    
    public static void AddExceptionMiddlleware(this WebApplication app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
    }
    
}
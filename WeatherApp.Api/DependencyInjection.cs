using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
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
    
    public static IServiceCollection AddTelemetryProvider(this IServiceCollection services, ILoggingBuilder loggingBuilder)
    {
        services.AddOpenTelemetry()
            .ConfigureResource(resource => resource.AddService("WeatherApp"))
            .WithMetrics(metrics =>
            {
                metrics
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation();

                metrics.AddOtlpExporter();
            })
            .WithTracing(tracing =>
            {
                tracing
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation();
                
                tracing.AddOtlpExporter();
            });

        loggingBuilder.AddOpenTelemetry(logging => logging.AddOtlpExporter());
        
        return services;
    }
    
    public static void AddExceptionMiddlleware(this WebApplication app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
    }
    
}
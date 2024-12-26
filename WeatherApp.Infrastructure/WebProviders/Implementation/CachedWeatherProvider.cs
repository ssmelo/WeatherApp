using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using WeatherApp.Application.Services;

namespace WeatherApp.Infrastructure.WebProviders;

public class CachedWeatherProvider : WeatherProvider
{
    private readonly WeatherProvider _provider;
    private readonly IDistributedCache _cache;

    public CachedWeatherProvider(WeatherProvider provider, IDistributedCache cache)
    {
        _provider = provider;
        _cache = cache;
    }

    public async Task<WeatherProviderResponse?> GetWeather(WeatherProviderParams weatherProviderParams)
    {
        if (!weatherProviderParams.Cache)
        {
            return await _provider.GetWeather(weatherProviderParams);
        }

        return await ProcessCacheResponse(weatherProviderParams);
    }

    private async Task<WeatherProviderResponse?> ProcessCacheResponse(WeatherProviderParams weatherProviderParams)
    {
        var cacheResponse = await _cache.GetStringAsync(weatherProviderParams.City);
        if (!string.IsNullOrWhiteSpace(cacheResponse))
        {
            return JsonSerializer.Deserialize<WeatherProviderResponse>(cacheResponse);
        }
        
        var result = await _provider.GetWeather(weatherProviderParams);
        
        _cache.SetStringAsync(
            weatherProviderParams.City,
            JsonSerializer.Serialize(result),
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(20)
            });
        
        return result;
    }
}
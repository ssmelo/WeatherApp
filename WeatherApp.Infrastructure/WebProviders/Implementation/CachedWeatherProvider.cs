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
        var cacheResponse = await _cache.GetStringAsync(weatherProviderParams.city);
        if (!string.IsNullOrWhiteSpace(cacheResponse))
        {
            return JsonSerializer.Deserialize<WeatherProviderResponse>(cacheResponse);
        }
        
        var result = await _provider.GetWeather(weatherProviderParams);
        
        _cache.SetStringAsync(
            weatherProviderParams.city,
            JsonSerializer.Serialize(result),
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(20)
            });
        
        return result;
    }
}
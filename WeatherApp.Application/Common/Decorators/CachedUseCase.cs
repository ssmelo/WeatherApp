using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using WeatherApp.Application.Services;

namespace WeatherApp.Application.Common.Decorators;

public class CachedUseCase<S, T> : IUseCase<S, T>
{
    private readonly IUseCase<S, T> _innerUseCase;
    private readonly IDistributedCache _distributedCache;

    public CachedUseCase(IUseCase<S, T> innerUseCase, IDistributedCache distributedCache)
    {
        _innerUseCase = innerUseCase;
        _distributedCache = distributedCache;
    }
    
    public async T Execute(S input)
    {
        var cacheResponse = await _distributedCache.GetStringAsync();
        if (!string.IsNullOrWhiteSpace(cacheResponse))
        {
            return JsonSerializer.Deserialize<WeatherProviderResponse>(cacheResponse);
        }
    }
}
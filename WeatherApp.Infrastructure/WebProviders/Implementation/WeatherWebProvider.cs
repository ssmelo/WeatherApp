using System.Net;
using System.Net.Http.Json;
using WeatherApp.Application.Services;

namespace WeatherApp.Infrastructure.WebProviders;

public class WeatherWebProvider : WeatherProvider
{
    private readonly IHttpClientFactory _httpClientFactory;

    public WeatherWebProvider(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    
    public async Task<WeatherProviderResponse?> GetWeather(WeatherProviderParams weatherProviderParams)
    {
        try
        {
            var client = _httpClientFactory.CreateClient("weather");

            var response = await client.GetAsync($"{weatherProviderParams.city}");

            return await HandleResponse(response);
        }
        catch (Exception exception)
        {
            throw new WeatherWebProviderException(exception.Message);
        }
    }

    private async Task<WeatherProviderResponse?> HandleResponse(HttpResponseMessage response)
    {
        if (HttpStatusCode.BadRequest.Equals(response.StatusCode))
        {
            return null;
        }

        if (!response.IsSuccessStatusCode)
        {
            throw new WeatherWebProviderException(await response.Content.ReadAsStringAsync());
        }
            
        return await response.Content.ReadFromJsonAsync<WeatherProviderResponse>();
    }
}
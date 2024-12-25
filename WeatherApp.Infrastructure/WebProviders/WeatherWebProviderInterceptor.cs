using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace WeatherApp.Infrastructure.WebProviders;

public class WeatherWebProviderInterceptor : DelegatingHandler
{
    private readonly IServiceProvider _serviceProvider;
    
    public WeatherWebProviderInterceptor(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (request.RequestUri is null)
        {
            return base.SendAsync(request, cancellationToken); //TODO Lançar exceção;
        }

        var weatherSettings = getWeatherSettings();
        
        request.RequestUri = buildUriWithWebServiceServiceKey(weatherSettings.ApiKey, request.RequestUri);

        return base.SendAsync(request, cancellationToken);
    }
    
    private WeatherSettings getWeatherSettings()
    {
        return _serviceProvider.GetRequiredService<IOptions<WeatherSettings>>().Value;
    }

    private Uri buildUriWithWebServiceServiceKey(string webServicekey, Uri baseRequestUri)
    {
        var pathUri = $"?unitGroup=metric&key={webServicekey}&contentType=json";
        
        StringBuilder requestStringBuilder = new StringBuilder();
        requestStringBuilder.Append(baseRequestUri);
        requestStringBuilder.Append(pathUri);
        
        return new Uri(requestStringBuilder.ToString());
    }
}
using WeatherApp.Application.Common;
using WeatherApp.Application.Services;

namespace WeatherApp.Application.Features;

public class GetWeatherForecastUseCase
{
     private readonly WeatherProvider _weatherProvider;

     public GetWeatherForecastUseCase(WeatherProvider weatherProvider)
     {
          _weatherProvider = weatherProvider;
     }
     public async Task<GetWeatherForecastResponse> Query(GetWeatherForecastQuery getWeatherForecastQuery)
     {
          var weatherResponse = await _weatherProvider.GetWeather(new WeatherProviderParams(getWeatherForecastQuery.City, getWeatherForecastQuery.Cache));

          if (weatherResponse is not null)
          {
               return new GetWeatherForecastResponse(weatherResponse.days);
          }
          
          throw new GetWeatherForecastException($"Weather forecast not found for the given city: {getWeatherForecastQuery.City}", ErrorType.NOT_FOUND);
     }
}
using Microsoft.AspNetCore.Mvc;
using WeatherApp.Application.Features;

namespace WeatherApp.Api.Controllers;

public static class WeatherEndpoints
{
    public static void AddWeatherEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/weatherforecast/{city}", async ([FromServices] GetWeatherForecastUseCase getWeatherForecast,
            [FromRoute] string city) =>
            {
                return Results.Ok(await getWeatherForecast.Query(new GetWeatherForecastQuery(city)));
            })
            .WithName("GetWeatherForecast")
            .WithOpenApi();
    }
}
using Microsoft.AspNetCore.Mvc;
using WeatherApp.Application.Features;

namespace WeatherApp.Api.Controllers;

public static class WeatherEndpoints
{
    public static void AddWeatherEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/weatherforecast/{city}", async ([FromServices] GetWeatherForecastUseCase getWeatherForecast,
                ILogger<Program> logger,
                [FromRoute] string city,
                [FromHeader] Boolean cache = true) =>
                {
                    logger.LogInformation("Receiving GET weatherforecast: {city}", city);
                    
                    var result = await getWeatherForecast.Query(new GetWeatherForecastQuery(city, cache));
                    
                    logger.LogInformation("Success GET weatherforecast: {city}", city);
                    return Results.Ok(result);
                })
                .WithName("GetWeatherForecast")
                .WithOpenApi();
    }
}
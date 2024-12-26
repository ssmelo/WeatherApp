using WeatherApp.Api;
using WeatherApp.Api.Controllers;
using WeatherApp.Application;
using WeatherApp.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services
        .AddInfrastructure(builder.Configuration)
        .AddPresentation()
        .AddApplication()
        .AddLoggingProvider()
        .AddCacheProvider(builder.Configuration)
        .AddTelemetryProvider(builder.Logging);
}

var app = builder.Build();
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.AddExceptionMiddlleware();

    app.UseHttpsRedirection();

    app.AddWeatherEndpoints();

    app.Run();
}

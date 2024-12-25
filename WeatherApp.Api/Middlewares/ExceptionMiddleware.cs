using System.Net;
using WeatherApp.Api.Errors;
using WeatherApp.Application.Common;

namespace WeatherApp.Api.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    
    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
    
    public async Task InvokeAsync(HttpContext httpContext, ILogger<ExceptionMiddleware> logger)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            await HandleExceptionAsync(httpContext, ex);
        }
    }
    
    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        
        if(exception is BaseApplicationException baseApplicationException)
        {
            context.Response.StatusCode = (int) mapResponseCode(baseApplicationException);
            await context.Response.WriteAsJsonAsync(new BaseErrorResponse
            {
                Message = baseApplicationException.Message
            });
            
            return;
        }

        context.Response.StatusCode = 500;
        await context.Response.WriteAsJsonAsync(new BaseErrorResponse
        {
            Message = "An unexpected error has occurred. Try again later."
        });
    }

    private HttpStatusCode mapResponseCode(BaseApplicationException baseApplicationException)
    {
        return baseApplicationException.Type switch
        {
            ErrorType.CONFLIT => HttpStatusCode.Conflict,
            ErrorType.NOT_FOUND => HttpStatusCode.NotFound,
            ErrorType.UNAUTHORIZED => HttpStatusCode.Unauthorized,
            _ => HttpStatusCode.InternalServerError
        };
    }
}
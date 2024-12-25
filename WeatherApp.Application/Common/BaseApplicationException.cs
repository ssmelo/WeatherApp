namespace WeatherApp.Application.Common;

public class BaseApplicationException : Exception
{
    public ErrorType? Type { get; init; }
    
    public BaseApplicationException(string message) : base(message)
    {
    }
    
    public BaseApplicationException(string message, ErrorType type) : base(message)
    {
        Type = type;
    }
}
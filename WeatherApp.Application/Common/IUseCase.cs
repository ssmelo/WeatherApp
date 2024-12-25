namespace WeatherApp.Application.Common;

public interface IUseCase<S, T>
{
    public T Execute(S input);
}
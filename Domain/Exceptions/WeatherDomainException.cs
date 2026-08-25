namespace Domain.Exceptions;

public class WeatherDomainException : Exception
{
    public WeatherDomainException() { }
    public WeatherDomainException(string message) : base(message) { }
    public WeatherDomainException(string message, Exception inner) : base(message, inner) { }
}
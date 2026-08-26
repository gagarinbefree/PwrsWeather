using Application.Dtos.WeatherApi;

namespace Application.Interfaces;

public interface IWeatherApiClient
{
    Task<WeatherDataDto> GetForecastAsync(double lat, double lon, int days);
}
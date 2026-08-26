using System.Text.Json;
using Application.Dtos.WeatherApi;
using Application.Interfaces;

namespace Infrastructure.Clients;

public class WeatherApiClient : IWeatherApiClient
{
    private readonly HttpClient _httpClient;

    public WeatherApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<WeatherDataDto> GetForecastAsync(double lat, double lon, int days)
    {
        // API работает без ключа
        var url = $"forecast.json?key=fa8b3df74d4042b9aa7135114252304&q={lat},{lon}&days={days}";
        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Failed to get forecast: {response.StatusCode}");
        }

        var json = await response.Content.ReadAsStringAsync();

        var dto = JsonSerializer.Deserialize<WeatherDataDto>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }) ?? throw new InvalidOperationException("Failed to deserialize forecast response");

        FillForecastData(dto, json);

        return dto;
    }

    private void FillForecastData(WeatherDataDto dto, string json)
    {
        var forecastRoot = JsonSerializer.Deserialize<ForecastRootDto>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (forecastRoot?.Forecast?.ForecastDay == null)
            return;

        var now = DateTime.Now;
        var today = now.Date;
        var tomorrow = today.AddDays(1);

        dto.HourlyForecast = forecastRoot.Forecast.ForecastDay
            .SelectMany(d => d.Hour)
            .Where(h =>
            {
                var time = h.Time;
                return (time.Date == today && time >= now) || time.Date == tomorrow;
            })
            .OrderBy(h => h.Time)
            .ToList();

        dto.DailyForecast = forecastRoot.Forecast.ForecastDay
            .Take(3)
            .ToList();
    }
}
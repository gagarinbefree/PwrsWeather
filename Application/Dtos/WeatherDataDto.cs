using System.Text.Json.Serialization;

namespace Application.Dtos.WeatherApi;

public class WeatherDataDto
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("region")]
    public string Region { get; set; } = string.Empty;

    [JsonPropertyName("country")]
    public string Country { get; set; } = string.Empty;

    [JsonPropertyName("lat")]
    public double Lat { get; set; }

    [JsonPropertyName("lon")]
    public double Lon { get; set; }

    [JsonPropertyName("tz_id")]
    public string Timezone { get; set; } = string.Empty;

    [JsonPropertyName("localtime")]
    public DateTime LocalTime { get; set; }

    [JsonPropertyName("current")]
    public CurrentWeatherDto? Current { get; set; }

    public List<HourlyForecastDto> HourlyForecast { get; set; } = new();
    public List<DailyForecastDto> DailyForecast { get; set; } = new();
}
using System.Text.Json.Serialization;
using Domain.Enums;

namespace Application.Dtos.WeatherApi;

public class DailyForecastDto
{
    [JsonPropertyName("date")]
    public DateTime Date { get; set; }

    [JsonPropertyName("day")]
    public DayInfoDto Day { get; set; } = new();

    [JsonPropertyName("astro")]
    public AstroInfoDto Astro { get; set; } = new();

    [JsonPropertyName("hour")]
    public List<HourlyForecastDto> Hour { get; set; } = new();  
}
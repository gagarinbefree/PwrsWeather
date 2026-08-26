using System.Text.Json.Serialization;

namespace Application.Dtos.WeatherApi;

public class ForecastRootDto
{
    [JsonPropertyName("forecast")]
    public ForecastContainerDto Forecast { get; set; } = new();
}

public class ForecastContainerDto
{
    [JsonPropertyName("forecastday")]
    public List<DailyForecastDto> ForecastDay { get; set; } = new();
}
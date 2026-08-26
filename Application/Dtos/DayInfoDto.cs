using System.Text.Json.Serialization;

namespace Application.Dtos.WeatherApi;

public class DayInfoDto
{
    [JsonPropertyName("maxtemp_c")]
    public double MaxTempC { get; set; }

    [JsonPropertyName("mintemp_c")]
    public double MinTempC { get; set; }

    [JsonPropertyName("avgtemp_c")]
    public double AvgTempC { get; set; }

    [JsonPropertyName("condition")]
    public ConditionDto Condition { get; set; } = new();

    [JsonPropertyName("maxwind_kph")]
    public double MaxWindKph { get; set; }

    [JsonPropertyName("totalprecip_mm")]
    public double TotalPrecipitationMm { get; set; }

    [JsonPropertyName("avghumidity")]
    public int AvgHumidity { get; set; }

    [JsonPropertyName("uv")]
    public double Uv { get; set; }

    [JsonPropertyName("daily_chance_of_rain")]
    public int DailyChanceOfRain { get; set; }

    [JsonPropertyName("sunrise")]
    public string Sunrise { get; set; } = string.Empty;

    [JsonPropertyName("sunset")]
    public string Sunset { get; set; } = string.Empty;
}

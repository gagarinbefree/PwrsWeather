using System.Text.Json.Serialization;


namespace Application.Dtos.WeatherApi;

public class AstroInfoDto
{
    [JsonPropertyName("sunrise")]
    public string Sunrise { get; set; } = string.Empty;

    [JsonPropertyName("sunset")]
    public string Sunset { get; set; } = string.Empty;
}
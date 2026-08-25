using Domain.Enums;

namespace Domain.Entities;

public class DailyForecast
{
    public DateTime Date { get; set; }
    public double MaxTempC { get; set; }
    public double MinTempC { get; set; }
    public double AvgTempC { get; set; }
    public WeatherCondition Condition { get; set; }
    public string ConditionText { get; set; } = string.Empty;
    public string ConditionIcon { get; set; } = string.Empty;
    public double MaxWindKph { get; set; }
    public double TotalPrecipitationMm { get; set; }
    public int AvgHumidity { get; set; }
    public double Uv { get; set; }
    public int DailyChanceOfRain { get; set; }
    public string Sunrise { get; set; } = string.Empty;
    public string Sunset { get; set; } = string.Empty;
}
using Domain.Enums;

namespace Domain.Entities;

public class CurrentWeather
{
    public DateTime LastUpdated { get; set; }     
    public double TemperatureC { get; set; }      
    public double FeelsLikeC { get; set; }        
    public WeatherCondition Condition { get; set; } 
    public string ConditionText { get; set; } = string.Empty;
    public string ConditionIcon { get; set; } = string.Empty;
    public double WindKph { get; set; }            
    public string WindDir { get; set; } = string.Empty;
    public int Humidity { get; set; }       
    public double PressureMb { get; set; }  
    public double PrecipitationMm { get; set; }  
    public int Cloud { get; set; }               
    public double Uv { get; set; }               
    public int IsDay { get; set; }               
    public double VisibilityKm { get; set; }     
    public int ChanceOfRain { get; set; }        
}
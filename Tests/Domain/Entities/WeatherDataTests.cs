using Domain.Entities;
using Xunit;

namespace Tests.Domain.Entities;

public class WeatherDataTests
{
    [Fact]
    public void WeatherData_ShouldInitializeCollections_WhenCreated()
    {
        var weatherData = new WeatherData();

        Assert.NotNull(weatherData.HourlyForecast);
        Assert.NotNull(weatherData.DailyForecast);
        Assert.Empty(weatherData.HourlyForecast);
        Assert.Empty(weatherData.DailyForecast);
    }

    [Fact]
    public void CurrentWeather_ShouldSetProperties_WhenInitialized()
    {
        var current = new CurrentWeather
        {
            TempC = 22.5,
            ConditionText = "Sunny",
            Humidity = 65,
            WindKph = 10.5
        };

        Assert.Equal(22.5, current.TempC);
        Assert.Equal("Sunny", current.ConditionText);
        Assert.Equal(65, current.Humidity);
        Assert.Equal(10.5, current.WindKph);
    }

    [Fact]
    public void DailyForecast_ShouldSetProperties_WhenInitialized()
    {
        var daily = new DailyForecast
        {
            Date = DateTime.Today,
            MaxtempC = 25.0,
            MintempC = 15.0,
            ConditionText = "Sunny",
            DailyChanceOfRain = 10
        };

        Assert.Equal(DateTime.Today, daily.Date);
        Assert.Equal(25.0, daily.MaxtempC);
        Assert.Equal(15.0, daily.MintempC);
        Assert.Equal("Sunny", daily.ConditionText);
        Assert.Equal(10, daily.DailyChanceOfRain);
    }

    [Fact]
    public void HourlyForecast_ShouldSetProperties_WhenInitialized()
    {
        var hourly = new HourlyForecast
        {
            Time = DateTime.Now,
            TempC = 20.0,
            ConditionText = "Cloudy",
            ChanceOfRain = 30
        };

        Assert.Equal(20.0, hourly.TempC);
        Assert.Equal("Cloudy", hourly.ConditionText);
        Assert.Equal(30, hourly.ChanceOfRain);
    }
}
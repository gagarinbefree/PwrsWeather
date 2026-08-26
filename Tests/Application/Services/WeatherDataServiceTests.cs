using Application.Dtos;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Mappers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace Tests.Application.Services;

public class WeatherDataServiceTests
{
    private readonly IMapper _mapper;
    private readonly WeatherDataService _service;

    public WeatherDataServiceTests()
    {
        var services = new ServiceCollection();
        services.AddSingleton<ILoggerFactory>(NullLoggerFactory.Instance);
        services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });
        var serviceProvider = services.BuildServiceProvider();
        _mapper = serviceProvider.GetRequiredService<IMapper>();
        _service = new WeatherDataService(_mapper);
    }

    [Fact]
    public void MapToWeatherData_ShouldCombineCurrentAndForecast_WhenValidDataProvided()
    {
        var current = new CurrentResponseDto
        {
            Location = new LocationDto
            {
                Name = "Moscow",
                Region = "Moscow Oblast",
                Country = "Russia",
                Lat = 55.7558,
                Lon = 37.6173,
                TzId = "Europe/Moscow",
                LocalTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm")
            },
            Current = new CurrentDataDto
            {
                LastUpdated = DateTime.Now.ToString("yyyy-MM-dd HH:mm"),
                TempC = 22.5,
                FeelslikeC = 20.0,
                Condition = new ConditionDto { Code = 1000, Text = "Sunny", Icon = "//cdn.weatherapi.com/weather/64x64/day/113.png" },
                WindKph = 10.5,
                WindDir = "N",
                Humidity = 65,
                PressureMb = 1012.0,
                PrecipMm = 0.0,
                Cloud = 10,
                Uv = 3.0,
                IsDay = 1,
                VisKm = 10.0,
                ChanceOfRain = 0
            }
        };

        var forecast = new ForecastResponseDto
        {
            Forecast = new ForecastDto
            {
                ForecastDay = new List<ForecastDayDto>
                {
                    new ForecastDayDto
                    {
                        Date = DateTime.Now.ToString("yyyy-MM-dd"),
                        Day = new DayDto
                        {
                            MaxtempC = 25.0,
                            MintempC = 15.0,
                            AvgtempC = 20.0,
                            Condition = new ConditionDto { Code = 1000, Text = "Sunny", Icon = "//cdn.weatherapi.com/weather/64x64/day/113.png" },
                            MaxwindKph = 12.0,
                            TotalprecipMm = 0.0,
                            Avghumidity = 60,
                            Uv = 5.0,
                            DailyChanceOfRain = 0
                        },
                        Astro = new AstroDto { Sunrise = "06:30 AM", Sunset = "08:00 PM" },
                        Hour = new List<HourDto>()
                    }
                }
            }
        };

        var result = _service.MapToWeatherData(current, forecast);

        Assert.NotNull(result);
        Assert.Equal("Moscow", result.Name);
        Assert.Equal("Russia", result.Country);
        Assert.NotNull(result.Current);
        Assert.Equal(22.5, result.Current.TempC);
        Assert.Equal("Sunny", result.Current.ConditionText);
        Assert.NotNull(result.DailyForecast);
        Assert.Single(result.DailyForecast);

        // Проверяем, что данные маппятся через DayDto
        var daily = result.DailyForecast[0];
        Assert.Equal(25.0, daily.MaxtempC); // теперь должно работать
        Assert.Equal(15.0, daily.MintempC);
        Assert.Equal(20.0, daily.AvgtempC);
        Assert.Equal("Sunny", daily.ConditionText);
        Assert.Equal("06:30 AM", daily.Sunrise);
        Assert.Equal("08:00 PM", daily.Sunset);
    }

    [Fact]
    public void MapToWeatherData_ShouldFilterHourlyForecast_ForTodayAndTomorrow()
    {
        var now = DateTime.Now;
        var today = now.Date;
        var tomorrow = today.AddDays(1);

        var current = new CurrentResponseDto
        {
            Location = new LocationDto { Name = "Moscow", Country = "Russia" },
            Current = new CurrentDataDto { TempC = 20.0 }
        };

        var forecast = new ForecastResponseDto
        {
            Forecast = new ForecastDto
            {
                ForecastDay = new List<ForecastDayDto>
                {
                    new ForecastDayDto
                    {
                        Date = today.ToString("yyyy-MM-dd"),
                        Hour = new List<HourDto>
                        {
                            new HourDto { Time = today.AddHours(8).ToString("yyyy-MM-dd HH:mm"), TempC = 18.0 },
                            new HourDto { Time = today.AddHours(12).ToString("yyyy-MM-dd HH:mm"), TempC = 22.0 },
                            new HourDto { Time = today.AddHours(16).ToString("yyyy-MM-dd HH:mm"), TempC = 20.0 }
                        }
                    },
                    new ForecastDayDto
                    {
                        Date = tomorrow.ToString("yyyy-MM-dd"),
                        Hour = new List<HourDto>
                        {
                            new HourDto { Time = tomorrow.AddHours(6).ToString("yyyy-MM-dd HH:mm"), TempC = 15.0 },
                            new HourDto { Time = tomorrow.AddHours(12).ToString("yyyy-MM-dd HH:mm"), TempC = 24.0 }
                        }
                    }
                }
            }
        };

        var result = _service.MapToWeatherData(current, forecast);

        Assert.NotNull(result);
        Assert.NotNull(result.HourlyForecast);
        Assert.Equal(2, result.HourlyForecast.Count);

        foreach (var hour in result.HourlyForecast)
        {
            Assert.True(
                (hour.Time.Date == today && hour.Time >= now) ||
                hour.Time.Date == tomorrow,
                $"Hour {hour.Time} is not in expected range");
        }
    }
}
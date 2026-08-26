using AutoMapper;
using Domain.Entities;
using Application.Dtos.WeatherApi;

namespace Infrastructure.Mappers;

public class WeatherMappingProfile : Profile
{
    public WeatherMappingProfile()
    {
        CreateMap<WeatherDataDto, WeatherData>();
        CreateMap<CurrentWeatherDto, CurrentWeather>();
        CreateMap<HourlyForecastDto, HourlyForecast>();
        CreateMap<DayInfoDto, DailyForecast>();
    }
}
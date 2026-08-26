using AutoMapper;
using Application.Dtos;
using Domain.Entities;

namespace Infrastructure.Mappers;

public class WeatherMappingProfile : Profile
{
    public WeatherMappingProfile()
    {
        CreateMap<LocationDto, WeatherData>()
            .ForMember(dest => dest.LocalTime, opt => opt.MapFrom(src =>
                string.IsNullOrEmpty(src.LocalTime) ? DateTime.Now : DateTime.Parse(src.LocalTime)));

        CreateMap<CurrentDataDto, CurrentWeather>()
            .ForMember(dest => dest.LastUpdated, opt => opt.MapFrom(src =>
                string.IsNullOrEmpty(src.LastUpdated) ? DateTime.Now : DateTime.Parse(src.LastUpdated)))
            .ForMember(dest => dest.ConditionCode, opt => opt.MapFrom(src => src.Condition.Code))
            .ForMember(dest => dest.ConditionText, opt => opt.MapFrom(src => src.Condition.Text))
            .ForMember(dest => dest.ConditionIcon, opt => opt.MapFrom(src => src.Condition.Icon));

        CreateMap<HourDto, HourlyForecast>()
            .ForMember(dest => dest.Time, opt => opt.MapFrom(src =>
                string.IsNullOrEmpty(src.Time) ? DateTime.Now : DateTime.Parse(src.Time)))
            .ForMember(dest => dest.ConditionCode, opt => opt.MapFrom(src => src.Condition.Code))
            .ForMember(dest => dest.ConditionText, opt => opt.MapFrom(src => src.Condition.Text))
            .ForMember(dest => dest.ConditionIcon, opt => opt.MapFrom(src => src.Condition.Icon));

        CreateMap<ForecastDayDto, DailyForecast>()
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src =>
                string.IsNullOrEmpty(src.Date) ? DateTime.Now : DateTime.Parse(src.Date)))
            .ForMember(dest => dest.ConditionCode, opt => opt.MapFrom(src => src.Day.Condition.Code))
            .ForMember(dest => dest.ConditionText, opt => opt.MapFrom(src => src.Day.Condition.Text))
            .ForMember(dest => dest.ConditionIcon, opt => opt.MapFrom(src => src.Day.Condition.Icon));
    }
}
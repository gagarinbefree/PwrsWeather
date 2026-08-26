using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Queries;

public class GetWeatherQueryHandler : IRequestHandler<GetWeatherQuery, WeatherData>
{
    private readonly IWeatherApiClient _weatherApiClient;
    private readonly IMapper _mapper;

    public GetWeatherQueryHandler(IWeatherApiClient weatherApiClient, IMapper mapper)
    {
        _weatherApiClient = weatherApiClient;
        _mapper = mapper;
    }

    public async Task<WeatherData> Handle(GetWeatherQuery request, CancellationToken cancellationToken)
    {
        // 1. Получаем DTO от API
        var dto = await _weatherApiClient.GetForecastAsync(
            request.Latitude,
            request.Longitude,
            3);

        // 2. Маппим в Domain
        var weatherData = _mapper.Map<WeatherData>(dto);

        // 3. Возвращаем результат
        return weatherData;
    }
}
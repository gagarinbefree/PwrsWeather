using Application.Interfaces;
using Infrastructure.Clients;
using Infrastructure.Mappers;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        string baseUrl)
    {
        // Register HttpClient
        services.AddHttpClient<IWeatherApiClient, WeatherApiClient>(client =>
        {
            client.BaseAddress = new Uri(baseUrl);
        });

        // Register AutoMapper
        services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<WeatherMappingProfile>();
        });

        return services;
    }
}
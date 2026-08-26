using Domain.Entities;
using MediatR;

namespace Application.Queries;

public record GetWeatherQuery(double Latitude, double Longitude) : IRequest<WeatherData>;
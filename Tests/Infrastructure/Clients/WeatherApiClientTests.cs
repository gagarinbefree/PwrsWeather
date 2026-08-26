using System.Net;
using System.Text.Json;
using Application.Dtos;
using Application.Interfaces;
using Infrastructure.Clients;
using Infrastructure.Configuration;
using Moq;
using Moq.Protected;
using Xunit;

namespace Tests.Infrastructure.Clients;

public class WeatherApiClientTests
{
    private readonly WeatherApiOptions _options;
    private readonly string _baseUrl = "http://api.weatherapi.com/v1/";
    private readonly string _apiKey = "test-api-key";
    private readonly string _location = "55.7558,37.6173";
    private readonly int _days = 3;

    public WeatherApiClientTests()
    {
        _options = new WeatherApiOptions
        {
            BaseUrl = _baseUrl,
            ApiKey = _apiKey,
            DefaultLocation = _location,
            ForecastDays = _days
        };
    }

    [Fact]
    public async Task GetCurrentWeatherAsync_ShouldReturnResponse_WhenApiCallSucceeds()
    {
        var jsonResponse = @"
        {
            ""location"": { ""name"": ""Moscow"", ""country"": ""Russia"" },
            ""current"": { ""temp_c"": 22.5, ""condition"": { ""text"": ""Sunny"", ""code"": 1000 } }
        }";

        var handlerMock = new Mock<HttpMessageHandler>();
        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(jsonResponse)
            });

        var httpClient = new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri(_baseUrl)
        };

        var client = new WeatherApiClient(httpClient, _options);
        var result = await client.GetCurrentWeatherAsync();

        Assert.NotNull(result);
        Assert.Equal("Moscow", result.Location.Name);
        Assert.Equal(22.5, result.Current.TempC);
    }

    [Fact]
    public async Task GetCurrentWeatherAsync_ShouldThrowException_WhenApiReturnsError()
    {
        var handlerMock = new Mock<HttpMessageHandler>();
        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.InternalServerError
            });

        var httpClient = new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri(_baseUrl)
        };

        var client = new WeatherApiClient(httpClient, _options);

        await Assert.ThrowsAsync<HttpRequestException>(() =>
            client.GetCurrentWeatherAsync());
    }

    [Fact]
    public async Task GetForecastAsync_ShouldReturnResponse_WhenApiCallSucceeds()
    {
        var jsonResponse = @"
        {
            ""location"": { ""name"": ""Moscow"", ""country"": ""Russia"" },
            ""forecast"": {
                ""forecastday"": [
                    { ""date"": ""2026-08-27"", ""day"": { ""maxtemp_c"": 25.0, ""mintemp_c"": 15.0 } }
                ]
            }
        }";

        var handlerMock = new Mock<HttpMessageHandler>();
        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(jsonResponse)
            });

        var httpClient = new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri(_baseUrl)
        };

        var client = new WeatherApiClient(httpClient, _options);
        var result = await client.GetForecastAsync();

        Assert.NotNull(result);
        Assert.Equal("Moscow", result.Location.Name);
        Assert.Single(result.Forecast.ForecastDay);
        Assert.Equal(25.0, result.Forecast.ForecastDay[0].Day.MaxtempC);
    }
}
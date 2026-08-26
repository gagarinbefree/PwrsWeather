using Application.Dtos;
using Application.Interfaces;
using Application.Queries;
using Moq;
using Xunit;

namespace Tests.Application.Handlers;

public class GetForecastQueryHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnForecast_WhenApiCallSucceeds()
    {
        var expectedResponse = new ForecastResponseDto
        {
            Location = new LocationDto { Name = "Moscow" },
            Forecast = new ForecastDto
            {
                ForecastDay = new List<ForecastDayDto>
                {
                    new ForecastDayDto { Date = DateTime.Now.ToString("yyyy-MM-dd") }
                }
            }
        };

        var apiClientMock = new Mock<IWeatherApiClient>();
        apiClientMock
            .Setup(x => x.GetForecastAsync())
            .ReturnsAsync(expectedResponse);

        var handler = new GetForecastQueryHandler(apiClientMock.Object);
        var query = new GetForecastQuery();

        var result = await handler.Handle(query, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal("Moscow", result.Location.Name);
        Assert.Single(result.Forecast.ForecastDay);
    }
}
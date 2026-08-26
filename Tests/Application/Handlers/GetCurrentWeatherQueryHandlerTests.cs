using Application.Dtos;
using Application.Interfaces;
using Application.Queries;
using Moq;
using Xunit;

namespace Tests.Application.Handlers;

public class GetCurrentWeatherQueryHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnCurrentWeather_WhenApiCallSucceeds()
    {
        var expectedResponse = new CurrentResponseDto
        {
            Location = new LocationDto { Name = "Moscow", Country = "Russia" },
            Current = new CurrentDataDto { TempC = 22.5 }
        };

        var apiClientMock = new Mock<IWeatherApiClient>();
        apiClientMock
            .Setup(x => x.GetCurrentWeatherAsync())
            .ReturnsAsync(expectedResponse);

        var handler = new GetCurrentWeatherQueryHandler(apiClientMock.Object);
        var query = new GetCurrentWeatherQuery();

        var result = await handler.Handle(query, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal("Moscow", result.Location.Name);
        Assert.Equal(22.5, result.Current.TempC);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenApiCallFails()
    {
        var apiClientMock = new Mock<IWeatherApiClient>();
        apiClientMock
            .Setup(x => x.GetCurrentWeatherAsync())
            .ThrowsAsync(new HttpRequestException("API error"));

        var handler = new GetCurrentWeatherQueryHandler(apiClientMock.Object);
        var query = new GetCurrentWeatherQuery();

        await Assert.ThrowsAsync<HttpRequestException>(() =>
            handler.Handle(query, CancellationToken.None));
    }
}
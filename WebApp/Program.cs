using Application.Extensions;
using Infrastructure.Configuration;
using Infrastructure.Extensions;
using WebApp.Components;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();

var options = new WeatherApiOptions
{
    BaseUrl = builder.Configuration["WeatherApi:BaseUrl"] ?? "",
    ApiKey = builder.Configuration["WeatherApi:ApiKey"] ?? "",
    DefaultLocation = builder.Configuration["WeatherApi:DefaultLocation"] ?? "",
    ForecastDays = int.Parse(builder.Configuration["WeatherApi:ForecastDays"] ?? "3")
};

builder.Services.AddInfrastructure(options);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
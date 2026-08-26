using Application.Extensions;
using Infrastructure.Extensions;
using WebApp.Components;

var builder = WebApplication.CreateBuilder(args);

// Add application services (MediatR)
builder.Services.AddApplication();

// Add infrastructure services
var baseUrl = builder.Configuration["WeatherApi:BaseUrl"]
    ?? throw new InvalidOperationException("WeatherApi:BaseUrl is not configured in appsettings.json");
//var apiKey = builder.Configuration["WeatherApi:ApiKey"]
//   ?? throw new InvalidOperationException("WeatherApi:ApiKey is not configured in appsettings.json");

builder.Services.AddInfrastructure(baseUrl);

// Add Blazor services
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
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
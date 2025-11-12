using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.Options;
using System.Globalization;
using System.Reflection;
using XE.DotNetConf2025.ApiService.Endpoints;
using XE.DotNetConf2025.Models;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails(options =>
{
    options.CustomizeProblemDetails = (context) =>
    {
        if (context.ProblemDetails is HttpValidationProblemDetails validationProblem)
        {
            context.ProblemDetails.Detail = $"Error(s) occurred: {validationProblem.Errors.Values.Sum(x => x.Length)}";

            // Converts the property keys on the errors object to comply with the JSON casing policy defined in serializer options.
            var namingPolicy = context.HttpContext.RequestServices.GetRequiredService<IOptions<JsonOptions>>().Value.SerializerOptions.PropertyNamingPolicy;
            if (namingPolicy is not null)
            {
                validationProblem.Errors = validationProblem.Errors
                    .ToDictionary(
                        kvp => namingPolicy.ConvertName(kvp.Key),
                        kvp => kvp.Value
                    );
            }
        }

        context.ProblemDetails.Extensions.TryAdd("timestamp", DateTime.UtcNow.ToString("o", CultureInfo.InvariantCulture));
    };
});


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddValidationForTypesInModels();
builder.Services.AddValidation();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options => {
        options.SwaggerEndpoint("/openapi/v1.json", "API Service V1");
    });
}

string[] summaries = ["Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"];

app.MapGet("/", () => "API service is running. Navigate to /weatherforecast to see sample data.");

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.MapSpeechesEnpoints();
app.MapDefaultEndpoints();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}


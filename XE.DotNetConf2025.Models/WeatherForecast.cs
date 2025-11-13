namespace XE.DotNetConf2025.Models;

using System;

/// <summary>
/// Represents a weather forecast for a specific date, including temperature and summary information.
/// </summary>
/// <param name="Date">The date for which the weather forecast applies.</param>
/// <param name="TemperatureC">The forecasted temperature in degrees Celsius for the specified date.</param>
/// <param name="Summary">A brief summary or description of the weather conditions for the specified date. Can be null if no summary is available.</param>
public record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    /// <summary>
    /// Gets the temperature in degrees Fahrenheit, calculated from the Celsius value.
    /// </summary>
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

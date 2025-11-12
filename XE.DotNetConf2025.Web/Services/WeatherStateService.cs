namespace XE.DotNetConf2025.Web.Services;

using Microsoft.AspNetCore.Components;

public class WeatherStateService
{
    [PersistentState]
    public IEnumerable<WeatherForecast>? Forecasts { get; set; }
}

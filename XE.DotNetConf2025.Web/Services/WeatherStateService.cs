namespace XE.DotNetConf2025.Web.Services;

using Microsoft.AspNetCore.Components;
using XE.DotNetConf2025.Models;

public class WeatherStateService
{
    [PersistentState]
    public IEnumerable<WeatherForecast>? Forecasts { get; set; }
}

using Microsoft.AspNetCore.Mvc;

namespace Medicine.Controllers;

[ApiController]
[Route("weatherforecast")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Issiq", "Sovuq", "Iliq", "Shamolli", "Yomg‘irli", "Qorli"
    };

    [HttpGet]
    public IEnumerable<WeatherForecast> Get()
    {
        var rng = new Random();
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.Parse(DateTime.Now.AddDays(index).ToString()),
            TemperatureC = rng.Next(-20, 45),
            Summary = Summaries[rng.Next(Summaries.Length)]
        })
        .ToArray();
    }
}

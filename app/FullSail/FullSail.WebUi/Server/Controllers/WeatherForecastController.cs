using FullSail.Shared;
using FullSail.WebUi.Shared;
using Microsoft.AspNetCore.Mvc;

namespace FullSail.WebUi.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly KodiClient _kodiClient;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, KodiClient kodiClient)
        {
            _logger = logger;
            _kodiClient = kodiClient;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
        [HttpPost]
        public async Task Test()
        {
            await _kodiClient.InputDownAsync();
        }
    }
}
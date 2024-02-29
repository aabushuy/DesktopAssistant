using Assistant.Domain.Weather;
using Microsoft.AspNetCore.Mvc;

namespace Assistant.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public WeatherForecast Get(byte mask = (byte)WeatherLevel.Now)
        {
            WeatherForecast forecast = new();

            if (IsMaskHasWeatherLevel(mask, WeatherLevel.Hour))
            {
                forecast.Hours = Array.Empty<WeatherHour>();
            }

            if (IsMaskHasWeatherLevel(mask, WeatherLevel.Day))
            {
                forecast.Days = Array.Empty<WeatherDay>();
            }

            return forecast;
        }

        private static bool IsMaskHasWeatherLevel(byte mask, WeatherLevel level)
        {
            return (byte)(mask & (byte)level) == (byte)level;
        }
    }
}

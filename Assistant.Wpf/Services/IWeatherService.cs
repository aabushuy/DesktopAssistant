using Assistant.Wpf.Models;
using System.Threading.Tasks;

namespace Assistant.Wpf.Services
{
    internal interface IWeatherService
    {
        Task<WeatherForecast> GetWeatherForecast();
    }
}

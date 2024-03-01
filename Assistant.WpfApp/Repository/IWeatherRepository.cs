using Assistant.Domain.Weather;

namespace Assistant.WpfApp.Repository;

public interface IWeatherRepository
{
    Task<WeatherForecast> GetWeatherForecast(WeatherLevel weatherLevel);
}
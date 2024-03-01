using Assistant.Domain.Weather;

namespace Assistant.WpfApp.Repository;

public class WeatherRepository : IWeatherRepository
{
    public Task<WeatherForecast> GetWeatherForecast(WeatherLevel weatherLevel)
    {
        var forecast = new WeatherForecast();

        return Task.FromResult(forecast);
    }
}
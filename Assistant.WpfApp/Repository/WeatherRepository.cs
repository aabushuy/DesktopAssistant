using Assistant.Domain.Weather;

namespace Assistant.WpfApp.Repository;

public class WeatherRepository : IWeatherRepository
{
    public Task<WeatherForecast> GetWeatherForecast()
    {
        var forecast = new WeatherForecast();

        return Task.FromResult(forecast);
    }
}
using Assistant.Wpf.Models;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Assistant.Wpf.Services
{
    internal class WeatherService : IWeatherService
    {       
        private const string _weatherApi = "http://api.openweathermap.org/data/2.5/onecall?appid=be204b616ebd7d06b88f022f37da0b45&units=metric&lat=54.62&lon=39.74&exlude=minutely,daily";

        private const int _cashIntervalMinutes = 15;
        private const int _fileCashIntervalMinutes = 30;
        private const string _weatherCacheFileName = "weather_cache";

        private DateTime _refreshDate;

        private WeatherForecast _cashedForecast;

        public WeatherService()
        {
            _refreshDate = DateTime.Now.AddHours(-3);
            _cashedForecast = new WeatherForecast();
        }

        public async Task<WeatherForecast> GetWeatherForecast()
        {
            if (_refreshDate.AddMinutes(_cashIntervalMinutes) < DateTime.Now)
            {
                await RefreshData();
            }

            return _cashedForecast;
        }

        private async Task RefreshData()
        {
            FileInfo cashFile = new(_weatherCacheFileName);

            bool needToUpload = !cashFile.Exists|| cashFile.CreationTime.AddMinutes(_fileCashIntervalMinutes) < DateTime.Now;

            if (needToUpload)
            {
                string httpData = await GetWeatherDataAsJsonString();

                await File.WriteAllTextAsync(_weatherCacheFileName, httpData);
            }

            string jsonData = await File.ReadAllTextAsync(_weatherCacheFileName);

            try
            {
                _cashedForecast = ParseJsonWeatherData(jsonData);
                _refreshDate = DateTime.Now;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private static async Task<string> GetWeatherDataAsJsonString()
        {
            using HttpClient httpClient = new();
            using HttpRequestMessage request = new(HttpMethod.Get, _weatherApi);

            HttpResponseMessage response = await httpClient.SendAsync(request);

            string content = await response.Content.ReadAsStringAsync();

            return content;
        }


        private static WeatherForecast ParseJsonWeatherData(string json)
        {
            var dynamicData = JsonConvert.DeserializeObject<dynamic>(json)!;

            var weatherForecast = new WeatherForecast()
            {
                Sunrise = GetTimeFromUtc((long)dynamicData.current.sunrise),
                Sunset = GetTimeFromUtc((long)dynamicData.current.sunset),
                Temperature = dynamicData.current.temp,
                FeelsLike = dynamicData.current.feels_like,
                Pressure = dynamicData.current.pressure,
                Humidity = dynamicData.current.humidity,
                WindSpeed = dynamicData.current.wind_speed,
                WeatherDescription = $"{dynamicData.current.weather[0].main}, {dynamicData.current.weather[0].description}",
            };

            ParseHourlyForecast(weatherForecast, dynamicData);
            ParseAlerts(weatherForecast, dynamicData);

            return weatherForecast;
        }

        private static void ParseHourlyForecast(WeatherForecast forecast, dynamic data)
        {
            foreach (var hourForecast in data.hourly)
            {
                forecast.Hourly.Add(new WeatherForecast()
                {
                    DT = GetTimeFromUtc((long)hourForecast.dt),
                    Temperature = hourForecast.temp,
                    Pop = hourForecast.pop * 100,
                });
            }
        }

        private static void ParseAlerts(WeatherForecast forecast, dynamic data)
        {
            var alerts = data.alerts ?? Array.Empty<dynamic>();

            foreach (var alert in alerts)
            {
                forecast.WeatherAlerts.Add(new WeatherAlert()
                {
                    StartDate = GetTimeFromUtc((long)alert.start),
                    EndDate = GetTimeFromUtc((long)alert.end),
                    EventName = alert.@event,
                    Description = alert.description,
                });
            }
        }


        private static DateTime GetTimeFromUtc(long seconds)
        {
            var startDt = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

            //offset
            startDt = startDt.AddSeconds(3 * 3600);

            return startDt.AddSeconds(seconds);
        }

    }
}

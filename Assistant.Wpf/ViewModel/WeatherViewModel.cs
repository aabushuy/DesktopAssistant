using Assistant.Wpf.Models;
using Assistant.Wpf.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assistant.Wpf.ViewModel
{
    public class WeatherViewModel : TimerViewModel
    {
        private readonly IWeatherService _weatherService;
        private WeatherForecast _forecast = new();
        private const int _hourlyCount = 6;


        private DateTime _updateTime;
        public string CurrentTemp => CastTemperatureToString(_forecast.Temperature);
        public string FeelsLike => $"Feels like: {CastTemperatureToString(_forecast.FeelsLike)}";
        public string WeatherDescription => _forecast.WeatherDescription;


        public DateTime UpdateTime
        {
            get => _updateTime;
            set
            {
                _updateTime = value;
                RaisePropertyChanged(nameof(UpdateTime));
            } 
        }
        public string CurrentWeatherDetails
        {
            get 
            { 
                StringBuilder sb = new();
                sb.AppendLine($"Sunrise:  {_forecast.Sunrise:HH:mm}");
                sb.AppendLine($"Sunset:   {_forecast.Sunset:HH:mm}");
                sb.AppendLine();
                sb.AppendLine($"Wind: {Math.Round(_forecast.WindSpeed)} m/s");
                sb.AppendLine($"Pressure: {GetPressureConverted(_forecast.Pressure)} mmHg");
                sb.AppendLine($"Humidity: {_forecast.Humidity} %");

                return sb.ToString();
            }            
        }

        public ObservableCollection<WeatherForecast> Hourly { get; private set; }

        public ObservableCollection<WeatherAlert> Alerts { get; private set; }

        public WeatherViewModel() : base(1000 * 15)
        {
            _weatherService = new WeatherService();

            _ = DoLoop();
        }

        protected override async Task DoLoop()
        {
            _forecast = await _weatherService.GetWeatherForecast();

            UpdateTime = DateTime.Now;

            Hourly = new ObservableCollection<WeatherForecast>(
                _forecast.Hourly
                    .Where(h => h.DT > UpdateTime && h.DT.Hour % 2 == 0)
                    .OrderBy(h => h.DT)
                    .Take(_hourlyCount));

            Alerts = new ObservableCollection<WeatherAlert>(
                _forecast.WeatherAlerts
                    .Where(w => !string.IsNullOrWhiteSpace(w.Description) && (w.StartDate > UpdateTime || w.EndDate > UpdateTime))
                    .OrderBy(w => w.StartDate)
                    .ThenBy(w => w.EndDate));

            RaisePropertyChanged(nameof(CurrentTemp));
            RaisePropertyChanged(nameof(FeelsLike));
            RaisePropertyChanged(nameof(WeatherDescription));
            RaisePropertyChanged(nameof(CurrentWeatherDetails));
            RaisePropertyChanged(nameof(Hourly));
            RaisePropertyChanged(nameof(Alerts));
        }

        private static string CastTemperatureToString(double temp)
        {
            var sign = temp > 0 
                ? "+" 
                : temp < 0 
                    ? "-"
                    : string.Empty;

            return $"{sign}{Math.Round(temp)}°C";
        }

        private static int GetPressureConverted(double hPa) => (int)Math.Round(hPa * 0.750062);
    }
}

using Assistant.Wpf.Helpers;
using Assistant.Wpf.Models;
using Assistant.Wpf.Services;
using System;
using System.Threading.Tasks;

namespace Assistant.Wpf.ViewModel
{
    public class ClockViewModel : TimerViewModel
    {
        private readonly IWeatherService _weatherService;

        private DateTime _currentTime;

        private WeatherForecast _forecast = new();

        public string TimeString => _currentTime.ToString("HH:mm");
        public string TimeSeconds => _currentTime.ToString("ss");
        public string DateString => _currentTime.ToString("d MMMM, yyyy - ddd").ToUpper();
        public string Sunrise => $"sunrise {_forecast.Sunrise:HH:mm}";
        public string Sunset => $"sunset {_forecast.Sunset:HH:mm}";        
        public string Temperature => $"{_forecast.Temperature.TempToString()}";        
        public string Wind => $"W: {Math.Round(_forecast.WindSpeed)} m/s";
        public string Pressure => $"P: {_forecast.Pressure.HPaToMmHg()} mmHg";        
        public string Humidity => $"H: {_forecast.Humidity} %";

        public string WeatherDescription => _forecast.WeatherDescription;
        public string FeelsLike => $"Feels like {_forecast.FeelsLike.TempToString()}";

        //TODO:
        public string MinMaxTemperature => $"{0}°C / {0}°C";

        private DateTime CurrentTime 
        {
            set 
            {
                _currentTime = value;
                RaisePropertyChanged(nameof(TimeString));
                RaisePropertyChanged(nameof(TimeSeconds));
                RaisePropertyChanged(nameof(DateString));

                RaisePropertyChanged(nameof(Sunrise));
                RaisePropertyChanged(nameof(Sunset));
                RaisePropertyChanged(nameof(Temperature));

                RaisePropertyChanged(nameof(Humidity));
                RaisePropertyChanged(nameof(Pressure));
                RaisePropertyChanged(nameof(Wind));

                RaisePropertyChanged(nameof(WeatherDescription));
                RaisePropertyChanged(nameof(MinMaxTemperature));
            }
        }


        public ClockViewModel() : base(1000)
        {
            _weatherService = new WeatherService();
        }

        protected override async Task DoLoop()
        {
            CurrentTime = DateTime.Now;

            _forecast = await _weatherService.GetWeatherForecast();
        }
    }
}

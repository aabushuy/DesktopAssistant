using System;
using System.Collections.Generic;

namespace Assistant.Wpf.Models
{
    public class WeatherForecast
    {
        public DateTime DT { get; init; }

        public string PrintDT => DT.ToString("HH");

        public DateTime Sunrise { get; init; }

        public DateTime Sunset { get; init; }

        public double Temperature { get; init; }
        
        public double FeelsLike { get; init; }

        public double Pressure { get; init; }

        public double Humidity { get; init; }

        public double WindSpeed { get; init; }

        public string WeatherDescription { get; init; } = string.Empty;

        public double Pop { get; init; }

        public List<WeatherForecast> Hourly { get; } = new List<WeatherForecast>();

        public List<WeatherAlert> WeatherAlerts { get; } = new List<WeatherAlert>();
    }
}

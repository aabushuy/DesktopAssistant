﻿namespace Assistant.Domain.Weather
{
    public class WeatherForecast
    {
        public DateTime ForecastTime { get; set; }
        public DateTime CreatedTime => DateTime.Now;        
        public IEnumerable<WeatherHour> Hours { get; set; }
        public IEnumerable<WeatherDay> Days { get; set; }
    }
}

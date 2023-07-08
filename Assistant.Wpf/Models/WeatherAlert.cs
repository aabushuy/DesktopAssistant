using System;

namespace Assistant.Wpf.Models
{
    public class WeatherAlert
    {
        public DateTime StartDate { get; init; }

        public DateTime EndDate { get; init; }

        public int HoursLeft => (int)(EndDate-DateTime.Now).TotalHours;

        public string EventName { get; init; }

        public string Description { get; init; }
    }
}
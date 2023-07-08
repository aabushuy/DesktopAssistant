using System;

namespace Assistant.Wpf.Models
{
    public class CalendarDay
    {
        private readonly DateTime _dateTime;

        public int Day => _dateTime.Day;

        public bool IsToday => _dateTime.Date == DateTime.Today;

        public CalendarDay(DateTime dateTime)
        {
            _dateTime = dateTime;
        }
    }
}

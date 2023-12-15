using Assistant.Wpf.Models;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Assistant.Wpf.ViewModel
{
    public class CalendarViewModel : TimerViewModel
    {
        private DateTime _selectedDate;
        private readonly CalendarDay[][] _monthDays = new CalendarDay[6][];

        public string MonthName => _selectedDate.ToString("MMMM").ToUpper();        

        public ObservableCollection<CalendarDay> FirstWeek => new(_monthDays[0]);

        public ObservableCollection<CalendarDay> SecondWeek => new(_monthDays[1]);

        public ObservableCollection<CalendarDay> ThirdWeek => new(_monthDays[2]);

        public ObservableCollection<CalendarDay> FourthWeek => new(_monthDays[3]);

        public ObservableCollection<CalendarDay> FifthWeek => new(_monthDays[4]);

        public ObservableCollection<CalendarDay> SixthWeek => new(_monthDays[5]);

        public CalendarViewModel():base(1000 * 3)
        {
            _ = DoLoop();
        }

        protected override Task DoLoop()
        {
            _selectedDate = DateTime.Now.Date.AddDays(-DateTime.Now.Day + 1);
            var cursorDate = _selectedDate;

            for (int weekIndex = 0; weekIndex < 6; weekIndex++)
            {
                _monthDays[weekIndex] = new CalendarDay[7];
                
                int dayIndex = weekIndex == 0
                    ? RuDayOfWeek(cursorDate.DayOfWeek)
                    : 0;

                for (; dayIndex < 7; dayIndex++)
                {
                    _monthDays[weekIndex][dayIndex] = new CalendarDay(cursorDate);

                    cursorDate = cursorDate.AddDays(1);
                }
            }

            RaisePropertyChanged(nameof(MonthName));
            RaisePropertyChanged(nameof(FirstWeek));            
            RaisePropertyChanged(nameof(SecondWeek));
            RaisePropertyChanged(nameof(ThirdWeek));
            RaisePropertyChanged(nameof(FourthWeek));
            RaisePropertyChanged(nameof(FifthWeek));
            RaisePropertyChanged(nameof(SixthWeek));

            return Task.CompletedTask;
        }

        private static int RuDayOfWeek(DayOfWeek dayOfWeek) 
            => (int)dayOfWeek switch
            {
                1 => 0,
                2 => 1,
                3 => 2,
                4 => 3,
                5 => 4,
                6 => 5,
                0 => 6,
                _ => throw new IndexOutOfRangeException($"DayOfWeek: {dayOfWeek}")
            };
    }
}



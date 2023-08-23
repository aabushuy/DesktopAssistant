using System;
using System.Threading.Tasks;

namespace Assistant.Wpf.ViewModel
{
    public class ClockViewModel : TimerViewModel
    {
        private DateTime _currentTime;

        public string TimeString => _currentTime.ToString("HH:mm");

        public string TimeAmPm => _currentTime.ToString("tt");

        public string TimeSeconds => _currentTime.ToString("ss");

        public string DateString => _currentTime.ToString("dddd, d");

        private DateTime CurrentTime 
        {
            set 
            {
                _currentTime = value;
                RaisePropertyChanged(nameof(TimeString));
                RaisePropertyChanged(nameof(TimeAmPm));
                RaisePropertyChanged(nameof(TimeSeconds));
                RaisePropertyChanged(nameof(DateString));
            }
        }


        public ClockViewModel() : base(1000)
        {            
        }

        protected override Task DoLoop()
        {
            CurrentTime = DateTime.Now;

            return Task.CompletedTask;
        }
    }
}

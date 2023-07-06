using System.Timers;

namespace Assistant.Wpf.ViewModel
{
    public abstract class TimerViewModel : ViewModelBase
    {
        private Timer _timer;

        public TimerViewModel(int interval = 1000)
        {
            _timer = new Timer()
            {
                Interval = interval,
                AutoReset = true,
                Enabled = true
            };

            _timer.Elapsed += DoLoop;
            _timer.Start();
        }

        protected abstract void DoLoop(object? sender, ElapsedEventArgs e);
    }
}

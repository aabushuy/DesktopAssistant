using System.Timers;
using Timer = System.Timers.Timer;

namespace Assistant.WpfApp.ViewModel;
public abstract class TimerViewModel : ViewModelBase
{
    private readonly Timer _timer;

    protected TimerViewModel(int interval = 1000)
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

    protected virtual void DoLoop(object? sender, ElapsedEventArgs e) => DoLoop();

    protected abstract Task DoLoop();
}
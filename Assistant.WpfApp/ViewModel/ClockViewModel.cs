using Assistant.WpfApp.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Assistant.WpfApp.ViewModel;

public class ClockViewModel : TimerViewModel
{
    private readonly IWeatherRepository _weatherRepository;
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
        _weatherRepository = App.AppHost.Services.GetRequiredService<IWeatherRepository>();
    }

    protected override Task DoLoop()
    {
        CurrentTime = DateTime.Now;

        return Task.CompletedTask;
    }
}
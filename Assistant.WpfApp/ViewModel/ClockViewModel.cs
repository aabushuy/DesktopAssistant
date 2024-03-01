using Assistant.WpfApp.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Assistant.WpfApp.ViewModel;

public class ClockViewModel : TimerViewModel
{
    private readonly IWeatherRepository _weatherRepository;

    private DateTime _currentTime;

    public string TimeString => _currentTime.ToString("HH:mm");

    public string TimeSeconds => _currentTime.ToString("ss");

    public string DateString => _currentTime.ToString("d MMMM, yyyy - ddd").ToUpper();
    
    public string Sunrise => $"sunrise {06:45}";
    public string Sunset => $"sunset {17:09}";
    public string Temperature => $"{15}\u00b0C";
    
    public string Humidity => $"H: {24}%";
    public string Pressure => $"P: {764} mmHg";
    public string Wind => $"W: {10} m/s";
    
    public string WeatherDescription => "Cloudy";
    public string FeelsLike => $"Feels like {10}\u00b0C";
    
    public string MinMaxTemperature => $"{5}\u00b0C / {17}\u00b0C";

    private DateTime CurrentTime 
    {
        set 
        {
            _currentTime = value;
            RaisePropertyChanged(nameof(TimeString));
            RaisePropertyChanged(nameof(TimeSeconds));
            RaisePropertyChanged(nameof(DateString));
        }
    }

    public ClockViewModel() : base(1000)
    {
        //= App.AppHost.Services.GetRequiredService<IWeatherRepository>()
    }

    protected override Task DoLoop()
    {
        CurrentTime = DateTime.Now;

        return Task.CompletedTask;
    }
}
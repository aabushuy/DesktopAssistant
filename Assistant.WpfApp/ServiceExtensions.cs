using Assistant.WpfApp.Repository;
using Assistant.WpfApp.ViewHelpers;
using Assistant.WpfApp.ViewModel;
using Assistant.WpfApp.Views;
using Microsoft.Extensions.DependencyInjection;

namespace Assistant.WpfApp;

public static class ServiceExtensions
{
    public static void AddWpf(this IServiceCollection services)
    {
        services
            .AddSingleton<MainWindow>();
        
        services
            .AddTransient<ISettingRepository, SettingRepository>()
            .AddTransient<IWeatherRepository, WeatherRepository>()
            .AddTransient<ClockViewModel>();
        
        services
            .AddFormFactory<MainSettingsWindow>();
    }

    private static void AddFormFactory<TForm>(this IServiceCollection services)
        where TForm : class
    {
        services
            .AddTransient<TForm>()
            .AddSingleton<Func<TForm>>(x => () => x.GetService<TForm>()!)
            .AddSingleton<IAbstractFactory<TForm>, AbstractFactory<TForm>>();
    }
}
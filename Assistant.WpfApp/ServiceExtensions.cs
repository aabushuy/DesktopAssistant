using Assistant.WpfApp.Repository;
using Assistant.WpfApp.ViewHelpers;
using Assistant.WpfApp.Views;
using Microsoft.Extensions.DependencyInjection;

namespace Assistant.WpfApp;

public static class ServiceExtensions
{
    public static IServiceCollection AddWpf(this IServiceCollection services)
    {
        return services
            .AddSingleton<MainWindow>()
            .AddFormFactory<MainSettingsWindow>()
            .AddTransient<ISettingRepository, SettingRepository>();
    }

    private static IServiceCollection AddFormFactory<TForm>(this IServiceCollection services)
        where TForm : class
    {
        return services
            .AddTransient<TForm>()
            .AddSingleton<Func<TForm>>(x => () => x.GetService<TForm>()!)
            .AddSingleton<IAbstractFactory<TForm>, AbstractFactory<TForm>>();
    }
}
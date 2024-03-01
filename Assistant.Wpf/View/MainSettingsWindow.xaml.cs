using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Assistant.Wpf.Consts;
using Assistant.Wpf.Extensions;
using Assistant.Wpf.Models;
using Assistant.Wpf.Services;
using WpfScreenHelper;

namespace Assistant.Wpf.Views;

public partial class MainSettingsWindow
{
    private readonly ISettingService _settingService;

    public MainSettingsWindow()
    {
        _settingService = new SettingService();

        InitializeComponent();

        Screens.ItemsSource = Screen.AllScreens;
        
        ReadSettings();
    }

    /// <summary>
    /// Save settings
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        await SaveSettings();
        
        DialogResult = true;
    }

    private async Task ReadSettings()
    {
        var settings = await _settingService.GetSettings();

        var selectedDisplay = settings.GetString(SettingNames.Display);
        Screens.SelectedItem = Screen.AllScreens
            .FirstOrDefault(s => s.DeviceName == selectedDisplay) ?? null;
        
        MainWindowWidth.Text = settings.GetString(SettingNames.MainWindowWidth);
        MainWindowHeight.Text = settings.GetString(SettingNames.MainWindowHeight);
        MainWindowMarginTop.Text = settings.GetString(SettingNames.MainWindowMarginTop);
        MainWindowMarginRight.Text = settings.GetString(SettingNames.MainWindowMarginRight);
    }
    
    private async Task SaveSettings()
    {
        var settings = new Settings();

        settings[SettingNames.Display] = (Screens.SelectedValue as Screen)?.DeviceName ?? string.Empty;

        settings[SettingNames.MainWindowWidth] = GetIntOrDefault(MainWindowWidth);
        settings[SettingNames.MainWindowHeight] = GetIntOrDefault(MainWindowHeight);
        settings[SettingNames.MainWindowMarginTop] = GetIntOrDefault(MainWindowMarginTop);
        settings[SettingNames.MainWindowMarginRight] = GetIntOrDefault(MainWindowMarginRight);
        
        await _settingService.SetSettings(settings);
    }

    private static int GetIntOrDefault(TextBox textBox, int defaultValue = default)
        => int.TryParse(textBox.Text, out var intValue) ? intValue : defaultValue;
}
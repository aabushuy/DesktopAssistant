using System.Windows;
using System.Windows.Controls;
using Assistant.WpfApp.Consts;
using Assistant.WpfApp.Extensions;
using Assistant.WpfApp.Model;
using Assistant.WpfApp.Repository;
using WpfScreenHelper;

namespace Assistant.WpfApp.Views;

public partial class MainSettingsWindow
{
    private readonly ISettingRepository _settingRepository;
    private readonly Settings _allSettings;

    public MainSettingsWindow(ISettingRepository settingRepository)
    {
        InitializeComponent();
        
        _settingRepository = settingRepository;

        Screens.ItemsSource = Screen.AllScreens;

        _allSettings = _settingRepository.GetSettings().Result;
        
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

    private void ReadSettings()
    {
        var selectedDisplay = _allSettings.GetString(SettingNames.Display);
        Screens.SelectedItem = Screen.AllScreens
            .FirstOrDefault(s => s.DeviceName == selectedDisplay) ?? null;
        
        MainWindowWidth.Text = _allSettings.GetString(SettingNames.MainWindowWidth);
        MainWindowHeight.Text =  _allSettings.GetString(SettingNames.MainWindowHeight);
        MainWindowMarginTop.Text =  _allSettings.GetString(SettingNames.MainWindowMarginTop);
        MainWindowMarginRight.Text =  _allSettings.GetString(SettingNames.MainWindowMarginRight);
        GlobalBackgroundTransparency.Text =  _allSettings.GetString(SettingNames.GlobalBackgroundTransparency);
    }
    
    private async Task SaveSettings()
    {
        _allSettings[SettingNames.Display] = (Screens.SelectedValue as Screen)?.DeviceName ?? string.Empty;
        
        _allSettings[SettingNames.MainWindowWidth] = GetIntOrDefault(MainWindowWidth);
        _allSettings[SettingNames.MainWindowHeight] = GetIntOrDefault(MainWindowHeight);
        _allSettings[SettingNames.MainWindowMarginTop] = GetIntOrDefault(MainWindowMarginTop);
        _allSettings[SettingNames.MainWindowMarginRight] = GetIntOrDefault(MainWindowMarginRight);
        _allSettings[SettingNames.GlobalBackgroundTransparency] = GetIntOrDefault(GlobalBackgroundTransparency);
        
        await _settingRepository.SetSettings(_allSettings);
    }

    private static int GetIntOrDefault(TextBox textBox, int defaultValue = default)
        => int.TryParse(textBox.Text, out var intValue) ? intValue : defaultValue;
}
using System.Windows;
using System.Windows.Controls;
using Assistant.WpfApp.Consts;
using Assistant.WpfApp.Extensions;
using Assistant.WpfApp.Model;
using Assistant.WpfApp.Repository;
using Assistant.WpfApp.ViewHelpers;
using WpfScreenHelper;

namespace Assistant.WpfApp.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
    private readonly IAbstractFactory<MainSettingsWindow> _mainSettingWindowFactory;
    private readonly ISettingRepository _settingRepository;

    public MainWindow(
        IAbstractFactory<MainSettingsWindow> mainSettingWindowFactory,
        ISettingRepository settingRepository)
    {
        _mainSettingWindowFactory = mainSettingWindowFactory;
        _settingRepository = settingRepository;

        InitializeComponent();
        
        var settings = _settingRepository.GetSettings().Result;
        
        ApplySettings(settings);
    }

    private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        var button = (Button)sender;
        switch (button.Name)
        {
            case "SettingsButton":
            {
                if (_mainSettingWindowFactory.Create().ShowDialog() ?? false)
                {
                    var settings = await _settingRepository.GetSettings();
                    
                    ApplySettings(settings);
                }
                break;
            }
            case "CloseButton":
            {
                var result = MessageBox.Show(
                    "Dow you wanna close the app?",
                    "Close app",
                    MessageBoxButton.OKCancel);
                
                if (result == MessageBoxResult.OK)
                {
                    Application.Current.Shutdown();
                }
                break;
            }
        }
    }

    private void ApplySettings(Settings settings)
    {
        var displayName = settings.GetStringOrDefault(SettingNames.Display);
        var screen = Screen.AllScreens.FirstOrDefault(s => s.DeviceName == displayName) ?? Screen.PrimaryScreen;
        
        var mainMargin = settings.GetIntOrDefault(SettingNames.MainWindowMargin);
        
        Width = settings.GetDoubleOrDefault(SettingNames.MainWindowWidth, Width);
        Height = settings.GetDoubleOrDefault(SettingNames.MainWindowHeight, Height);
        
        Top = 0;
        Left = screen.WorkingArea.Right - Width - mainMargin;
        
        //TODO: GlobalBackgroundTransparency
    }
}
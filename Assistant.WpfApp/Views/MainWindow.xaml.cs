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
        var displayName = settings.GetString(SettingNames.Display);
        var screen = Screen.AllScreens.FirstOrDefault(s => s.DeviceName == displayName) ?? Screen.PrimaryScreen;
        
        var mainMarginTop = settings.GetInt(SettingNames.MainWindowMarginTop);
        var mainMarginRight = settings.GetInt(SettingNames.MainWindowMarginRight);
        
        Width = settings.GetDouble(SettingNames.MainWindowWidth, Width);
        Height = settings.GetDouble(SettingNames.MainWindowHeight, Height);
        
        Top = mainMarginTop;
        Left = screen.WorkingArea.Right - Width - mainMarginRight;
        
        //TODO: GlobalBackgroundTransparency
    }
}
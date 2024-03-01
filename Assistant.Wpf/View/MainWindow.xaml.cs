using Assistant.Wpf.Consts;
using Assistant.Wpf.Extensions;
using Assistant.Wpf.Models;
using Assistant.Wpf.Services;
using Assistant.Wpf.Views;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfScreenHelper;

namespace Assistant.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ISettingService _settingService;

        public MainWindow()
        {
            _settingService = new SettingService();

            InitializeComponent();

            var settings = _settingService.GetSettings().Result;

            ApplySettings(settings);
        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            switch (button.Name)
            {
                case "SettingsButton":
                    {
                        var settingWindow = new MainSettingsWindow();
                        if (settingWindow.ShowDialog() ?? false)
                        {
                            var settings = await _settingService.GetSettings();

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
        }
    }
}

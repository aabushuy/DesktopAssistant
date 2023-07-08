using System.Linq;
using System.Windows;
using WpfScreenHelper;

namespace Assistant.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int _marginWindow = 40;

        public MainWindow()
        {
            InitializeComponent();

            var selectedScreen = Screen.AllScreens.First();

            Left = selectedScreen.WorkingArea.Right - Width - _marginWindow;
        }
    }
}

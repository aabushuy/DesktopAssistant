using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Pomodoro
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TimerState _timerState = TimerState.Stop;

        public MainWindow()
        {
            InitializeComponent();
            FillComboBoxes();
            SetControlState();
        } 

        #region Private
        private void StartPauseClick(object sender, RoutedEventArgs e)
        {
            _timerState = _timerState == TimerState.Stop
                ? TimerState.Run
                : _timerState == TimerState.Pause
                    ? TimerState.Run
                    : TimerState.Pause;

            SetControlState();
        }

        private void StopClick(object sender, RoutedEventArgs e)
        {
            _timerState = TimerState.Stop;

            SetControlState();
        }

        private void SetControlState()
        {
            StopButton.IsEnabled = _timerState == TimerState.Run || _timerState == TimerState.Pause;

            WorkTimeComboBox.IsEnabled = _timerState == TimerState.Stop;
            RestTimeComboBox.IsEnabled = _timerState == TimerState.Stop;
            RoundsComboBox.IsEnabled = _timerState == TimerState.Stop;

            if (_timerState == TimerState.Run)
            {
                StartButton.Content = "Pause";
                StartButton.Background = Brushes.LightSkyBlue;
                StartButton.Foreground = Brushes.Black;
            }
            else
            {
                StartButton.Content = "Start";
                StartButton.Background = Brushes.Green;
                StartButton.Foreground = Brushes.White;
            }

            TimerTextBlock.Text = _timerState == TimerState.Stop ? string.Empty : TimerTextBlock.Text;
            switch (_timerState)
            {
                case TimerState.Stop:
                    TimerStatusTextBlock.Text = string.Empty;
                    break;
                case TimerState.Run:
                    TimerStatusTextBlock.Text = "Run";
                    break;
                case TimerState.Pause:
                    TimerStatusTextBlock.Text = "Pause";
                    break;
            }
        }

        private void FillComboBoxes()
        {
            WorkTimeComboBox.Items.Clear();
            _ = new int[] { 25, 30, 45 }.Select(x => WorkTimeComboBox.Items.Add(x)).ToArray();
            WorkTimeComboBox.SelectedIndex = 0;

            RestTimeComboBox.Items.Clear();
            _ = new int[] { 5, 10, 15 }.Select(x => RestTimeComboBox.Items.Add(x)).ToArray();
            RestTimeComboBox.SelectedIndex = 0;

            RoundsComboBox.Items.Clear();
            _ = Enumerable.Range(1, 6).Select(x => RoundsComboBox.Items.Add(x)).ToArray();
            RoundsComboBox.SelectedIndex = 3;
        }
        #endregion
    }
}
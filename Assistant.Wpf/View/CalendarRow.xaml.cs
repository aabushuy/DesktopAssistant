using Assistant.Wpf.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Assistant.Wpf.View
{
    /// <summary>
    /// Interaction logic for CalendarRow.xaml
    /// </summary>
    public partial class CalendarRow : UserControl
    {
        public ObservableCollection<CalendarDay> Week
        {
            get => (ObservableCollection<CalendarDay>)GetValue(WeekProperty);
            set => SetValue(WeekProperty, value);
        }

        public static readonly DependencyProperty WeekProperty;

        static CalendarRow()
        {
            WeekProperty = DependencyProperty.Register(
                "Week",
                typeof(ObservableCollection<CalendarDay>),
                typeof(CalendarRow));
        }

        public CalendarRow()
        {
            InitializeComponent();
        }
    }
}

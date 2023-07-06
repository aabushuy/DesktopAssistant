using System.Collections.ObjectModel;
using System.Linq;
using WpfScreenHelper;

namespace Assistant.Wpf.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly ObservableCollection<Screen> _screens;
        private Screen _selectedScreen;

        public Screen SelectedScreen 
        {
            get => _selectedScreen;
            set
            {
                _selectedScreen = value;
                RaisePropertyChanged(nameof(SelectedScreen));                
            }
        }

        public ReadOnlyObservableCollection<Screen> Screens => new(_screens);

        public MainWindowViewModel()
        {
            _screens = new ObservableCollection<Screen>(Screen.AllScreens);

            SelectedScreen = Screens.First();
        }
    }
}


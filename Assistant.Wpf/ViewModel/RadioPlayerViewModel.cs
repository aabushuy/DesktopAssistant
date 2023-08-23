using Assistant.Wpf.Models;
using Assistant.Wpf.Services;
using System.Collections.ObjectModel;

namespace Assistant.Wpf.ViewModel
{
    public class RadioPlayerViewModel : ViewModelBase
    {
        private readonly IRadioService _radioService;

        private RelayCommand _playStopCommand;

        private ObservableCollection<RadioStation> _radioStations;

        public string SelectedStationName => SelectedStation.Name;
        public string PlayPauseButtonText => _radioService.IsPlaying ? "Stop" : "Play";

        public RadioStation SelectedStation
        { 
            get => _radioService.SelectedStation;
            set 
            {
                _radioService.SetStation(value);
                
                RaisePropertyChanged(nameof(SelectedStation));
                RaisePropertyChanged(nameof(SelectedStationName));
            }
        }

        public RelayCommand PlayStopCommand => _playStopCommand ??= new RelayCommand(obj =>
        {
            if (_radioService.IsPlaying)
                _radioService.Stop();
            else
                _radioService.Play();

            RaisePropertyChanged(nameof(PlayPauseButtonText));
        });


        public ObservableCollection<RadioStation> RadioStations
        {
            get => _radioStations;
            set
            {
                _radioStations = value;
                RaisePropertyChanged(nameof(RadioStations));
            }
        }

        public RadioPlayerViewModel()
        {
            _radioService = new RadioService();

            RaisePropertyChanged(nameof(SelectedStationName));

            RadioStations = new ObservableCollection<RadioStation>(_radioService.GetRadioStations());
        }
    }
}

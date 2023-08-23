using Assistant.Wpf.Models;
using System.Collections.Generic;

namespace Assistant.Wpf.Services
{
    internal interface IRadioService
    {
        RadioStation SelectedStation { get; }

        bool IsPlaying { get; }

        IEnumerable<RadioStation> GetRadioStations();

        void SetStation(RadioStation station);

        void Play();

        void Stop();
    }
}

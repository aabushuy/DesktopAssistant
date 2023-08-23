using Assistant.Wpf.Models;
using System.Collections.Generic;
using System.Linq;

namespace Assistant.Wpf.Services
{
    internal class RadioService : IRadioService
    {
        public RadioStation SelectedStation { get; private set; }

        public bool IsPlaying { get; private set; }

        public RadioService()
        {
            SelectedStation = GetRadioStations().First();
        }

        public IEnumerable<RadioStation> GetRadioStations()
        {
            return new List<RadioStation>()
            {
                new RadioStation()
                {
                    Name = "Radio Maximum",
                },
                new RadioStation()
                {
                    Name = "Питер FM",
                },
                new RadioStation()
                {
                    Name = "DFM",
                }
            };

        }

        public void SetStation(RadioStation station)
        {
            if (SelectedStation is not null)
            {
                Stop();
            }

            SelectedStation = station;
        }

        public void Play()
        {
            if (SelectedStation is not null)
            {
                IsPlaying = true;
            }
        }

        public void Stop()
        {
            if (SelectedStation is not null && IsPlaying)
            {
                IsPlaying = false;
            }
        }
    }
}

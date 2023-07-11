using Assistant.Wpf.Models;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Assistant.Wpf.ViewModel
{
    public class HardDiskViewModel : TimerViewModel
    {
        private ObservableCollection<HardDriveInfo> _drives;
        public ObservableCollection<HardDriveInfo> DriveInfos
        { 
            get => _drives;
            set 
            {
                _drives = value;
                RaisePropertyChanged(nameof(DriveInfos));
            }
        }

        public HardDiskViewModel() : base(1000 * 60)
        {
            DriveInfos = new ObservableCollection<HardDriveInfo>();
            DoLoop();
        }

        protected override async Task DoLoop()
        {
            var drives = DriveInfo
                .GetDrives()
                .Where(d => d.IsReady)
                .Select(d => new HardDriveInfo(d));

            DriveInfos = new ObservableCollection<HardDriveInfo>(drives);
        }
    }
}

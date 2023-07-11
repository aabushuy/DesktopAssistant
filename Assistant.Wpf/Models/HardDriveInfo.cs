using System;
using System.IO;

namespace Assistant.Wpf.Models
{
    public class HardDriveInfo
    {
        private DriveInfo DriveInfo { get; }

        public string Name => $"{DriveInfo.VolumeLabel} ({DriveInfo.Name})";

        public string SizeText => $"{GetDiskSizeText(DriveInfo.AvailableFreeSpace)} free of {GetDiskSizeText(DriveInfo.TotalSize)}";

        public int Occupancy => (int)((DriveInfo.TotalSize - DriveInfo.AvailableFreeSpace) * 100 / DriveInfo.TotalSize);

        public HardDriveInfo(DriveInfo driveInfo)
        {
            DriveInfo = driveInfo;
        }

        private string GetDiskSizeText(long bytes, int iteration = 0)
        {
            var size = bytes / 1024;
            if (size > 0)
            {
                return GetDiskSizeText(size, ++iteration);
            }

            return iteration switch
            {
                0 => $"{bytes} bytes",
                1 => $"{bytes} KB",
                2 => $"{bytes} MB",
                3 => $"{bytes} GB",
                4 => $"{bytes} TB",
                _ => throw new NotImplementedException(),
            };
        }
    }
}

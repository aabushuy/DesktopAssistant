using Assistant.Wpf.Models;
using System.IO;
using System.Threading.Tasks;
using static System.Text.Json.JsonSerializer;

namespace Assistant.Wpf.Services
{
    internal class SettingService : ISettingService
    {
        private const string SettingFileName = "ui_settings.json";

        public async Task<Settings> GetSettings()
        {
            var fi = new FileInfo(SettingFileName);
            if (!fi.Exists)
            {
                await SetSettings(new Settings());
            }

            await using var fsRead = new FileStream(SettingFileName, FileMode.OpenOrCreate);

            return await DeserializeAsync<Settings>(fsRead) ?? new Settings();
        }

        public async Task SetSettings(Settings settings)
        {
           await using var fwWrite = new FileStream(SettingFileName, FileMode.OpenOrCreate);

            await SerializeAsync(fwWrite, settings);
        }
    }
}

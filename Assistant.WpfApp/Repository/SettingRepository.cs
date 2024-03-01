using System.IO;
using Assistant.WpfApp.Model;
using static System.Text.Json.JsonSerializer;

namespace Assistant.WpfApp.Repository;

public class SettingRepository : ISettingRepository
{
    private const string SettingFileName = "ui_settings.json";
    
    private static Settings _settings = new();

    public SettingRepository()
    {
        var fi = new FileInfo(SettingFileName);
        if (!fi.Exists)
        {
            SetSettings(_settings);
        }

        ReadFromFile();
    }

    public Task<Settings> GetSettings() => Task.FromResult(_settings);

    public async Task SetSettings(Settings settings)
    {
        _settings = settings;
        await SaveToFile();
    }

    private static async Task ReadFromFile()
    {
        await using var fsRead = new FileStream(SettingFileName, FileMode.OpenOrCreate);
        _settings = await DeserializeAsync<Settings>(fsRead) ?? new Settings();
    }

    private static async Task SaveToFile()
    {
        await using var fwWrite = new FileStream(SettingFileName, FileMode.OpenOrCreate);
        await SerializeAsync(fwWrite, _settings);
    }
}
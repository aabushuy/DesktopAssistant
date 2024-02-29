using Assistant.WpfApp.Model;

namespace Assistant.WpfApp.Repository;

public class SettingRepository : ISettingRepository
{
    private static Settings _settings = new();
    
    public Task<Settings> GetSettings()
    {
        return Task.FromResult(_settings);
    }

    public Task SetSettings(Settings settings)
    {
        _settings = settings;

        return Task.CompletedTask;
    }
}
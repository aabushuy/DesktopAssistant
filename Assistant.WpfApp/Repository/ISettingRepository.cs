using Assistant.WpfApp.Model;

namespace Assistant.WpfApp.Repository;

public interface ISettingRepository
{
    Task<Settings> GetSettings();
    
    Task SetSettings(Settings settings);
}
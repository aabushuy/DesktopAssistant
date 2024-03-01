using Assistant.WpfApp.Model;

namespace Assistant.WpfApp.Extensions;

public static class SettingExtensions
{
    public static string GetString(this Settings settings, string name, string defaultValue = "")
        => settings[name].ToString() ?? defaultValue;
    
    public static int GetInt(this Settings settings, string name, int defaultValue = 0)
        => int.TryParse(settings[name].ToString(), out var parsed) ? parsed : defaultValue;
    
    public static double GetDouble(this Settings settings, string name, double defaultValue = 0)
        => double.TryParse(settings[name].ToString(), out var parsed) ? parsed : defaultValue;
    
    public static Settings GetSection(this Settings settings, string name)
        => (Settings)settings[name];
}
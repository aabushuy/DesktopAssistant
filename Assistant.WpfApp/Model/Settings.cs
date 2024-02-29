using Assistant.WpfApp.Consts;

namespace Assistant.WpfApp.Model;

public class Settings
{
    private readonly Dictionary<string, object?> _children = new();

    public object this[string key]
    {
        get => _children.GetValueOrDefault(key) ?? new object();
        set => _children[key] = value;
    }

    public Settings()
    {
        //defaults
        this[SettingNames.MainWindowWidth] = 350;
        this[SettingNames.MainWindowHeight] = 1000;
        this[SettingNames.MainWindowMargin] = 40;
    }
}
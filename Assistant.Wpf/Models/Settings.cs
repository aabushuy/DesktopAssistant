using Assistant.Wpf.Consts;
using System.Collections.Generic;

namespace Assistant.Wpf.Models;

public class Settings
{
    public Dictionary<string, object?> Children { get; set; } = new();

    public object this[string key]
    {
        get => Children.GetValueOrDefault(key) ?? new object();
        set => Children[key] = value;
    }

    public Settings()
    {
        //defaults
        this[SettingNames.MainWindowWidth] = 300;
        this[SettingNames.MainWindowHeight] = 1040;
        this[SettingNames.MainWindowMarginRight] = 20;
        this[SettingNames.MainWindowMarginTop] = 0;
        this[SettingNames.Display] = @"\.\DISPLAY1";
    }
}
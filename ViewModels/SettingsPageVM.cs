using AverageAssistant.Appthemes;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace AverageAssistant.SettingsPageVM;

public partial class SettingPageVM:ObservableObject
{
    [ObservableProperty]
    public string selectedAppTheme = string.Empty;
    [ObservableProperty]
    public string selectedDefaultAverageSystem = string.Empty;

    public ObservableCollection<string> AppthemPicker { get; } = new ObservableCollection<string>()
    {
        "Dark",
        "Light"
    };

    public ObservableCollection<string> DefaultAverageSystemPicker { get; } = new ObservableCollection<string>()
    {
        "English",
        "Hungarian",
        "Romanian"
    };

    partial void OnSelectedAppThemeChanged(string value)
    {
        if (value == null)
            return;

        ResourceDictionary dict;

        switch(value)
        {
            case "Dark":
                dict = new DarkTheme();
                App.Current!.UserAppTheme = AppTheme.Dark;
                break;
            default:
                dict = new LightTheme();
                App.Current!.UserAppTheme = AppTheme.Light;
                break;
        }
        Application.Current.Resources.MergedDictionaries.Clear();
        Application.Current.Resources.MergedDictionaries.Add(dict);
    }

}
/*-----------------------------
Project name  : Average Assistant
Developer     : Benjamin Man
Project start : 15. 09. 2025
Project end   : 25. 10. 2025
Main purpose  : Help students calculate and manage their averages easily.
------------------------------*/

/*---------------------
Main task of the file

This ViewModel manages the settings that the user might modify.
----------------------- */


using AverageAssistant.Appthemes;
using AverageAssistant.Services;
using JsonManagement;

using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace AverageAssistant.SettingsPageVM;

public partial class SettingPageVM:ObservableObject
{

    //If there are any modifications, this method will load it from a JSON.
    public async Task LoadSettingsFromFile()
    {
        var FileHandler = new JsonManager();
        var FileSetting = await ((IJsonManager)FileHandler).ReadFromFileSettings();
        SelectedAppTheme = FileSetting!;
        OnPropertyChanged(nameof(SelectedAppTheme));
    }

    
    //Here the program sets the app theme. After setting it, the method calls the 'CreateFileForSettings' method 

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


        var FileHandler = new JsonManager();
        ((IJsonManager)FileHandler).CreateFileForSettings(SelectedAppTheme);


    }

    [ObservableProperty]
    public string selectedAppTheme = string.Empty;

    public ObservableCollection<string> AppthemPicker { get; } = new ObservableCollection<string>()
    {
        "Dark",
        "Light"
    };

}
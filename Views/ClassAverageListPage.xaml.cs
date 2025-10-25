using AverageAssistant.ViewModels;
using AverageAssistant.SettingsPageVM;

namespace AverageAssistant;

public partial class ClassAverageListPage : ContentPage
{
    public ClassAverageListPage()
    {
        InitializeComponent();
        
        var vm = new CaViewModel();
        BindingContext = vm;

        _ = vm.LoadRecordsFormFiles();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var Settings = new SettingPageVM();
        await Settings.LoadSettingsFromFile();
    }

}
using AverageAssistant.ViewModels;

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

}
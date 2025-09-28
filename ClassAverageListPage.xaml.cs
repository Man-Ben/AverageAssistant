namespace AverageAssistant;
using AverageAssistant.ViewModels;
public partial class ClassAverageListPage : ContentPage
{
    public ClassAverageListPage()
    {
        InitializeComponent();
        this.BindingContext = new caViewModel();
    }
}
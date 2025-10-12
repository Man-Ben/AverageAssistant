namespace AverageAssistant;
using AverageAssistant.ViewModels;
public partial class AddPage : ContentPage
{
    public AddPage()
    {
        InitializeComponent();
        this.BindingContext = new AddPageTools();
        
    }
    
}
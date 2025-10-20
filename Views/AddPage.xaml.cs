using AverageAssistant.ViewModels;
using AverageAssistant.Models;

namespace AverageAssistant;
public partial class AddPage : ContentPage
{
    public AddPage()
    {
        InitializeComponent();
        var record = new Record();
        this.BindingContext = new AddPageTools();
        
    }
    
}
using AverageAssistant.ViewModels;
using AverageAssistant.Models;

namespace AverageAssistant.Views;

public partial class EditPage : ContentPage
{
	public EditPage()
	{
		InitializeComponent();
        this.BindingContext = new EditPageVM();
    }
}
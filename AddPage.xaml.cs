namespace AverageAssistant;

public partial class AddPage : ContentPage
{
	public AddPage()
	{
		InitializeComponent();
		this.BindingContext = new AddPageTools();
    }
}
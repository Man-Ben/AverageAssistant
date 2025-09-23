namespace AverageAssistant;

public partial class ClassAverageListPage : ContentPage
{
	public ClassAverageListPage()
	{
		InitializeComponent();
		this.BindingContext = new caViewModel();
    }
}
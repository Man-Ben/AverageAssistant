using AverageAssistant.ViewModels;
using AverageAssistant.RecordsVM;
using System.Threading.Tasks;

namespace AverageAssistant;

public partial class ClassAverageListPage : ContentPage
{
    public ClassAverageListPage()
    {
        InitializeComponent();
        this.BindingContext = new CaViewModel();
    }

}
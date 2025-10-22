using AverageAssistant.SettingsPageVM;

namespace AverageAssistant
{
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
            this.BindingContext = new SettingPageVM();
        }
    }
}

using FullSail.ViewModels;

namespace FullSail.Views;

public partial class SettingsPage : ContentPage
{
    public SettingsPage()
    {
        InitializeComponent();

        BindingContext = new SettingsViewModel();
    }
}
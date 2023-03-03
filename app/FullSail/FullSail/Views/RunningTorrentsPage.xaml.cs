using FullSail.ViewModels;

namespace FullSail.Views;

public partial class RunningTorrentsPage : ContentPage
{
    public RunningTorrentsPage()
    {
        InitializeComponent();

        BindingContext = new RunningTorrentsViewModel();
    }
}
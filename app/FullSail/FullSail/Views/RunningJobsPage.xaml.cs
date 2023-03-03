using FullSail.ViewModels;

namespace FullSail.Views;

public partial class RunningJobsPage : ContentPage
{
    public RunningJobsPage()
    {
        InitializeComponent();

        BindingContext = new RunningsJobsViewModel();
    }
}
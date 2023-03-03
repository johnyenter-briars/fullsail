using FullSail.ViewModels;

namespace FullSail.Views;

public partial class RunningJobsPage : ContentPage
{
    public RunningJobsPage()
    {
        InitializeComponent();

        BindingContext = new RunningsJobsViewModel();
    }
    protected override void OnAppearing()
    {
        Task.Run(async () =>
        {
            var bc = (RunningsJobsViewModel)BindingContext;

            await bc?.Refresh();
        });
    }
}
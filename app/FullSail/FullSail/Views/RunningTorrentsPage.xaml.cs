using FullSail.ViewModels;

namespace FullSail.Views;

public partial class RunningTorrentsPage : ContentPage
{
    public RunningTorrentsPage()
    {
        InitializeComponent();

        BindingContext = new RunningTorrentsViewModel();

    }
    protected override void OnAppearing()
    {
        Task.Run(async () =>
        {
            var bc = (RunningTorrentsViewModel)BindingContext;

            await bc?.Refresh();
        });
    }
}
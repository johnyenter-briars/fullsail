using FullSail.ViewModels;

namespace FullSail.Views;

public partial class FilesInMediaSystemPage : ContentPage
{
    public FilesInMediaSystemPage()
    {
        InitializeComponent();

        BindingContext = new FilesInMediaSystemViewModel();
    }
    protected override void OnAppearing()
    {
        Task.Run(async () =>
        {
            var bc = (FilesInMediaSystemViewModel)BindingContext;

            await bc?.Refresh();
        });
    }
}
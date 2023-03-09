using CommunityToolkit.Maui.Views;
using FullSail.ViewModels;

namespace FullSail.Views;

public partial class FilesInMediaStorePage : ContentPage
{
    public FilesInMediaStorePage()
    {
        InitializeComponent();
        BindingContext = new FilesInMediaStoreViewModel();
    }
    protected override void OnAppearing()
    {
        Task.Run(async () =>
        {
            var bc = (FilesInMediaStoreViewModel)BindingContext;
            await bc.Refresh();
        });
    }
}
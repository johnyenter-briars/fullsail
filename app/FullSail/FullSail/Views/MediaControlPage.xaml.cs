using FullSail.ViewModels;

namespace FullSail.Views;

public partial class MediaControlPage : ContentPage
{
    private KodiClient kodiClient;
    public MediaControlPage()
    {
        InitializeComponent();

        BindingContext = new MediaControlViewModel();
    }
}
using FullSail.ViewModels;

namespace FullSail.Views;

public partial class TorrentSearchPage : ContentPage
{
    public TorrentSearchPage()
    {
        InitializeComponent();

        BindingContext = new TorrentSearchViewModel();
    }
    private void ExecuteTorrentSelectedCommand(object sender, EventArgs e)
    {
        var foo = 10;
    }
}
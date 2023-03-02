using FullSail.ViewModels;
using System.Diagnostics;

namespace FullSail.Views;

public partial class TorrentSearchPage : ContentPage
{
    public TorrentSearchPage()
    {
        InitializeComponent();

        BindingContext = new TorrentSearchViewModel();
    }
}
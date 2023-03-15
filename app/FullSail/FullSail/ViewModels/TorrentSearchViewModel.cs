using FullSail.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FullSail.ViewModels;
internal class TorrentSearchViewModel : BaseViewModel
{
    private List<TorrentSearchResult> searchResults = new();
    public List<TorrentSearchResult> SearchResults
    {
        get { return searchResults; }
        set { SetProperty(ref searchResults, value); }
    }
    public ICommand PerformSearch => new Command<string>(async (string query) =>
    {
        ToggleIsFetchingData();
        SearchResults = await FullSailClientSingleton.GetTorrentSearchResults(query, selectedSearchSite);
        ToggleIsFetchingData();
    });
    public TorrentSearchWebsite selectedSearchSite = TorrentSearchWebsite.solid;
    public string SelectedSearchSite
    {
        get { return selectedSearchSite.ToString(); }
        set
        {
            _ = Enum.TryParse(value, out TorrentSearchWebsite website);
            SetProperty(ref selectedSearchSite, website);
        }
    }
    public ICommand AddTorrentCommand => new Command<TorrentSearchResult>(async (searchResult) =>
    {
        bool startTorrent = await AlertServiceSingleton.ShowConfirmationAsync("Confirm", "Are you sure you want to start this torrent?", "Yes", "No");

        if (startTorrent)
        {
            var response = await FullSailClientSingleton.StartTorrent(searchResult.MagnetLink);

            await AlertServiceSingleton.ShowAlertAsync("Success", "Torrent added successfully");
        }
    });
    private void ToggleIsFetchingData()
    {
        FetchingData = !FetchingData;
        NotFetchingData = !NotFetchingData;
    }
    private bool fetchingData = false;
    public bool FetchingData
    {
        get { return fetchingData; }
        set { SetProperty(ref fetchingData, value); }
    }
    private bool notFetchingData = true;
    public bool NotFetchingData
    {
        get { return notFetchingData; }
        set { SetProperty(ref notFetchingData, value); }
    }
}

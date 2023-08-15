using FullSail.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FullSail.ViewModels;
internal class SubTitleSearchViewModel : BaseViewModel
{
    private List<SubtitleSearchResult> searchResults = new();
    public List<SubtitleSearchResult> SearchResults
    {
        get { return searchResults; }
        set { SetProperty(ref searchResults, value); }
    }
    public ICommand PerformSearch => new Command<string>(async (string query) =>
    {
        ToggleIsFetchingData();
        SearchResults = await FullSailClientSingleton.GetSubtitleSearchResults(query, selectedSearchSite);
        ToggleIsFetchingData();
    });
    public SubtitleSearchWebsite selectedSearchSite = SubtitleSearchWebsite.opensubtitles;
    public string SelectedSearchSite
    {
        get { return selectedSearchSite.ToString(); }
        set
        {
            _ = Enum.TryParse(value, out SubtitleSearchWebsite website);
            SetProperty(ref selectedSearchSite, website);
        }
    }
    public ICommand DownloadSubtitleCommand => new Command<SubtitleSearchResult>(async (searchResult) =>
    {
        var message = await FullSailClientSingleton.DownloadSubtitle(searchResult, selectedSearchSite);

        if (message != null)
        {
            await AlertServiceSingleton.ShowAlertAsync("Success", "Subtitle downloaded.");
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

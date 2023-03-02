using FullSail.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FullSail.ViewModels
{
    internal class TorrentSearchViewModel : BaseViewModel
    {
        public TorrentSearchViewModel()
        {
            Task.Run(async () =>
            {
                SearchResults = await FullSailClientSingleton.GetTorrentSearchResults("ant man and the wasp", selectedSearchSite);
            });
        }
        private List<TorrentSearchResult> searchResults = new();
        public List<TorrentSearchResult> SearchResults
        {
            get { return searchResults; }
            set { SetProperty(ref searchResults, value); }
        }
        public ICommand PerformSearch => new Command<string>(async (string query) =>
        {
            SearchResults = await FullSailClientSingleton.GetTorrentSearchResults(query, selectedSearchSite);
        });
        public TorrentSearchWebsite selectedSearchSite = TorrentSearchWebsite.solid;
        public string SelectedSearchSite
        {
            get { return selectedSearchSite.ToString(); }
            set
            {
                Enum.TryParse(value, out TorrentSearchWebsite website);
                SetProperty(ref selectedSearchSite, website);
            }
        }
        public ICommand AddTorrentCommand => new Command<TorrentSearchResult>(async (searchResult) =>
        {
            bool startTorrent = await AlertServiceSingleton.ShowConfirmationAsync("Confirm", "Are you sure you want to start this torrent?", "Yes", "No");

            if (startTorrent)
            {
                var torrentClient = DependencyService.Get<FullSailClient>();

                var response = await torrentClient.StartTorrent(searchResult.MagnetLink);

                await AlertServiceSingleton.ShowAlertAsync("Success", "Torrent added successfully");
            }
        });
    }
}

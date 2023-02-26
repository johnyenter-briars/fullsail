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
        }
        private List<SearchResult> searchResults = new();
        public List<SearchResult> SearchResults
        {
            get { return searchResults; }
            set { SetProperty(ref searchResults, value); }
        }
        public ICommand PerformSearch => new Command<string>(async (string query) =>
        {
            SearchResults = await FullSailClientSingleton.GetTorrentSearchResults(query, selectedSearchSite);
        });
        public SearchWebsite selectedSearchSite;
        public string SelectedSearchSite
        {
            get { return selectedSearchSite.ToString(); }
            set
            {
                var foo = Enum.TryParse(value, out SearchWebsite website);
                SetProperty(ref selectedSearchSite, website);
            }
        }
    }
}

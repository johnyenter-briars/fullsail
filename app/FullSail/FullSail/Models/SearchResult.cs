using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullSail.Models
{
    public class SearchResult
    {
        [JsonProperty(PropertyName = "torrent_name")]
        public string TorrentName { get; set; }
        [JsonProperty(PropertyName = "number_seeders")]
        public int NumberSeeders { get; set; }
    }
}

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
        [JsonProperty(PropertyName = "magnet_link")]
        public string MagnetLinnk { get; set; }

        [JsonProperty(PropertyName = "number_seeders")]
        public string NumberSeeders { get; set; }

        [JsonProperty(PropertyName = "number_leechers")]
        public string NumberLeechers { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "number_downloads")]
        public string NumberDownloads { get; set; }

        [JsonProperty(PropertyName = "size")]
        public string Size { get; set; }

        [JsonProperty(PropertyName = "date_posted")]
        public string DatePosted { get; set; }
    }
}

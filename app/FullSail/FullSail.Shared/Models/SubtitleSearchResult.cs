using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullSail.Shared.Models;

public class SubtitleSearchResult
{
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }
    [JsonProperty(PropertyName = "full_link")]
    public string FullLink { get; set; }

    [JsonProperty(PropertyName = "date_posted")]
    public string DatePosted { get; set; }
}

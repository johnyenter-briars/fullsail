using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullSail.Models;
// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public class QBTFile
{
    [JsonProperty("added_on")]
    public long AddedOn { get; set; }

    [JsonProperty("amount_left")]
    public long AmountLeft { get; set; }

    [JsonProperty("auto_tmm")]
    public bool AutoTmm { get; set; }

    [JsonProperty("availability")]
    public double Availability { get; set; }

    [JsonProperty("category")]
    public string Category { get; set; }

    [JsonProperty("completed")]
    public long Completed { get; set; }

    [JsonProperty("completion_on")]
    public long CompletionOn { get; set; }

    [JsonProperty("content_path")]
    public string ContentPath { get; set; }

    [JsonProperty("dl_limit")]
    public long DlLimit { get; set; }

    [JsonProperty("dlspeed")]
    public long Dlspeed { get; set; }

    [JsonProperty("download_path")]
    public string DownloadPath { get; set; }

    [JsonProperty("downloaded")]
    public long Downloaded { get; set; }

    [JsonProperty("downloaded_session")]
    public long DownloadedSession { get; set; }

    [JsonProperty("eta")]
    public long Eta { get; set; }

    [JsonProperty("f_l_piece_prio")]
    public bool FLPiecePrio { get; set; }

    [JsonProperty("force_start")]
    public bool ForceStart { get; set; }

    [JsonProperty("hash")]
    public string Hash { get; set; }

    [JsonProperty("infohash_v1")]
    public string InfohashV1 { get; set; }

    [JsonProperty("infohash_v2")]
    public string InfohashV2 { get; set; }

    [JsonProperty("last_activity")]
    public long LastActivity { get; set; }

    [JsonProperty("magnet_uri")]
    public string MagnetUri { get; set; }

    [JsonProperty("max_ratio")]
    public long MaxRatio { get; set; }

    [JsonProperty("max_seeding_time")]
    public long MaxSeedingTime { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("num_complete")]
    public long NumComplete { get; set; }

    [JsonProperty("num_incomplete")]
    public long NumIncomplete { get; set; }

    [JsonProperty("num_leechs")]
    public long NumLeechs { get; set; }

    [JsonProperty("num_seeds")]
    public long NumSeeds { get; set; }

    [JsonProperty("priority")]
    public long Priority { get; set; }

    [JsonProperty("progress")]
    public double Progress { get; set; }

    [JsonProperty("ratio")]
    public double Ratio { get; set; }

    [JsonProperty("ratio_limit")]
    public long RatioLimit { get; set; }

    [JsonProperty("save_path")]
    public string SavePath { get; set; }

    [JsonProperty("seeding_time")]
    public long SeedingTime { get; set; }

    [JsonProperty("seeding_time_limit")]
    public long SeedingTimeLimit { get; set; }

    [JsonProperty("seen_complete")]
    public long SeenComplete { get; set; }

    [JsonProperty("seq_dl")]
    public bool SeqDl { get; set; }

    [JsonProperty("size")]
    public long Size { get; set; }
    public long SizeGB => Size / 1000000000;

    [JsonProperty("state")]
    public string State { get; set; }

    [JsonProperty("super_seeding")]
    public bool SuperSeeding { get; set; }

    [JsonProperty("tags")]
    public string Tags { get; set; }

    [JsonProperty("time_active")]
    public long TimeActive { get; set; }

    [JsonProperty("total_size")]
    public long TotalSize { get; set; }

    [JsonProperty("tracker")]
    public string Tracker { get; set; }

    [JsonProperty("trackers_count")]
    public long TrackersCount { get; set; }

    [JsonProperty("up_limit")]
    public long UpLimit { get; set; }

    [JsonProperty("uploaded")]
    public long Uploaded { get; set; }

    [JsonProperty("uploaded_session")]
    public long UploadedSession { get; set; }

    [JsonProperty("upspeed")]
    public long Upspeed { get; set; }
}


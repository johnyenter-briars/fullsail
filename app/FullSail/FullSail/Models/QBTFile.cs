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
    public int AddedOn { get; set; }

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
    public int CompletionOn { get; set; }

    [JsonProperty("content_path")]
    public string ContentPath { get; set; }

    [JsonProperty("dl_limit")]
    public int DlLimit { get; set; }

    [JsonProperty("dlspeed")]
    public int Dlspeed { get; set; }

    [JsonProperty("download_path")]
    public string DownloadPath { get; set; }

    [JsonProperty("downloaded")]
    public long Downloaded { get; set; }

    [JsonProperty("downloaded_session")]
    public int DownloadedSession { get; set; }

    [JsonProperty("eta")]
    public int Eta { get; set; }

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
    public int LastActivity { get; set; }

    [JsonProperty("magnet_uri")]
    public string MagnetUri { get; set; }

    [JsonProperty("max_ratio")]
    public int MaxRatio { get; set; }

    [JsonProperty("max_seeding_time")]
    public int MaxSeedingTime { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("num_complete")]
    public int NumComplete { get; set; }

    [JsonProperty("num_incomplete")]
    public int NumIncomplete { get; set; }

    [JsonProperty("num_leechs")]
    public int NumLeechs { get; set; }

    [JsonProperty("num_seeds")]
    public int NumSeeds { get; set; }

    [JsonProperty("priority")]
    public int Priority { get; set; }

    [JsonProperty("progress")]
    public double Progress { get; set; }

    [JsonProperty("ratio")]
    public double Ratio { get; set; }

    [JsonProperty("ratio_limit")]
    public int RatioLimit { get; set; }

    [JsonProperty("save_path")]
    public string SavePath { get; set; }

    [JsonProperty("seeding_time")]
    public int SeedingTime { get; set; }

    [JsonProperty("seeding_time_limit")]
    public int SeedingTimeLimit { get; set; }

    [JsonProperty("seen_complete")]
    public int SeenComplete { get; set; }

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
    public int TimeActive { get; set; }

    [JsonProperty("total_size")]
    public long TotalSize { get; set; }

    [JsonProperty("tracker")]
    public string Tracker { get; set; }

    [JsonProperty("trackers_count")]
    public int TrackersCount { get; set; }

    [JsonProperty("up_limit")]
    public int UpLimit { get; set; }

    [JsonProperty("uploaded")]
    public int Uploaded { get; set; }

    [JsonProperty("uploaded_session")]
    public int UploadedSession { get; set; }

    [JsonProperty("upspeed")]
    public int Upspeed { get; set; }
}


using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullSail.Shared.Models;
public class QBTResponse
{
	[JsonProperty("running_torrents")]
	public List<QBTFile> RunningTorrents { get; set; }
}

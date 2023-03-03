using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullSail.Models
{
    public class UpdateTorrentsRequest
    {
        public List<string> Hashes { get; set; }
    }
}

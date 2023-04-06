using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullSail.Models
{
    public class MediaFile
    {

        public string Name { get; set; }
        public string FullPath => $"media-root{Name}";
        public string ShortName { get { return Name.Split('/').LastOrDefault(); } }
        public string ParentFolder => FullPath.Replace($"/{ShortName}", "");
        public int? Duration { get; set; } = 15;
        public bool IsFile { get; set; }
        public bool IsFolder => !IsFile; 
    }
}

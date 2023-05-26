using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullSail.Models;
internal class UpdateFileMediaSystemRequest
{
	public string FileName { get; set; }
	public string Destination { get; set; }
	public ComputerDestination ComputerDestination { get; set; }
}
public enum ComputerDestination
{
	Laptop,
	MediaStore,
}

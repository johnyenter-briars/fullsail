using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullSail.Models;
public class HealthCheckResponse
{
    public string NordStatus { get; set; }
    public string Country { get; set; }
    public string CpuUsed { get; set; }
    public string MemoryUsed { get; set; }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FullSail.ViewModels;
internal class RunningsJobsViewModel : BaseViewModel
{
    public async Task Refresh()
    {
        Jobs = (await FullSailClientSingleton.GetRunningJobs()).Jobs;
    }

    private List<string> jobs = new();

    public List<string> Jobs
    {
        get { return jobs; }
        set { SetProperty(ref jobs, value); }
    }
}

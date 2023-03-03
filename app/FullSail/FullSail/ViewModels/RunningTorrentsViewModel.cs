using FullSail.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullSail.ViewModels;
internal class RunningTorrentsViewModel : BaseViewModel
{
    public RunningTorrentsViewModel()
    {
        Task.Run(async () =>
        {
            var foo = await FullSailClientSingleton.GetRunningTorrents();

            RunningTorrents = foo.RunningTorrents;
        });
    }
    private List<QBTFile> runningTorrents = new();

    public List<QBTFile> RunningTorrents
    {
        get { return runningTorrents; }
        set { SetProperty(ref runningTorrents, value); }
    }
}

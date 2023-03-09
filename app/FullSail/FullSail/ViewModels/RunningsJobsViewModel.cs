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
        ToggleIsFetchingData();
        Jobs = (await FullSailClientSingleton.GetRunningJobs()).Jobs;
        ToggleIsFetchingData();
    }
    public ICommand RefreshCommand => new Command(async () => { await Refresh(); });

    private List<string> jobs = new();

    public List<string> Jobs
    {
        get { return jobs; }
        set { SetProperty(ref jobs, value); }
    }
    private void ToggleIsFetchingData()
    {
        FetchingData = !FetchingData;
        NotFetchingData = !NotFetchingData;
    }
    private bool fetchingData = false;

    public bool FetchingData
    {
        get { return fetchingData; }
        set { SetProperty(ref fetchingData, value); }
    }
    private bool notFetchingData = true;
    public bool NotFetchingData
    {
        get { return notFetchingData; }
        set { SetProperty(ref notFetchingData, value); }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FullSail.ViewModels;
internal class HealthCheckViewModel : BaseViewModel
{
    public async Task Refresh()
    {
        ToggleIsFetchingData();

        var healthCheck = await FullSailClientSingleton.HealthCheck();
        NordStatus = healthCheck.NordStatus;
        Country = healthCheck.Country;
        CpuUsed = healthCheck.CpuUsed;
        MemoryUsed = healthCheck.MemoryUsed;

        ToggleIsFetchingData();
    }
    public ICommand RefreshCommand => new Command(async () => { await Refresh(); });

    private string nordStatus = "";

    public string NordStatus
    {
        get { return nordStatus; }
        set { SetProperty(ref nordStatus, value); }
    }
    private string country = "";

    public string Country
    {
        get { return country; }
        set { SetProperty(ref country, value); }
    }
    private string cpuUsed = "";
    public string CpuUsed
    {
        get { return cpuUsed; }
        set { SetProperty(ref cpuUsed, value); }
    }
    private string memoryUsed = "";
    public string MemoryUsed
    {
        get { return memoryUsed; }
        set { SetProperty(ref memoryUsed, value); }
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

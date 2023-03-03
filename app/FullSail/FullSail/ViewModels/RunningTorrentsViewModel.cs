using FullSail.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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
    public ICommand ResumeTorrent => new Command<QBTFile>(async (torrentFile) =>
    {
        await FullSailClientSingleton.ResumeTorrent(torrentFile.Hash);
    });
    public ICommand PauseTorrent => new Command<QBTFile>(async (torrentFile) =>
    {
        await FullSailClientSingleton.PauseTorrent(torrentFile.Hash);
    });
    public ICommand DeleteTorrent => new Command<QBTFile>(async (torrentFile) =>
    {
        bool deleteTorrent = await AlertServiceSingleton.ShowConfirmationAsync("Confirm", "Are you SURE you want to delete this torrent?", "Yes", "No");

        if (deleteTorrent)
        {
            await FullSailClientSingleton.DeleteTorrent(torrentFile.Hash);
        }
    });
}

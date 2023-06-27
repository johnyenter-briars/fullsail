using FullSail.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FullSail.ViewModels;
internal class RunningTorrentsViewModel : BaseViewModel
{
	public async Task Refresh()
	{
		ToggleIsFetchingData();
		var response = await FullSailClientSingleton.GetRunningTorrents();

		RunningTorrents = response.RunningTorrents;
		ToggleIsFetchingData();
	}
	public ICommand RefreshCommand => new Command(async () => { await Refresh(); });

	private List<QBTFile> runningTorrents = new();

	public List<QBTFile> RunningTorrents
	{
		get { return runningTorrents; }
		set { SetProperty(ref runningTorrents, value); }
	}
	public ICommand ResumeTorrent => new Command<QBTFile>(async (torrentFile) =>
	{
		await FullSailClientSingleton.ResumeTorrent(torrentFile.Hash);

		await Task.Delay(1000);

		await Refresh();
	});
	public ICommand PauseTorrent => new Command<QBTFile>(async (torrentFile) =>
	{
		await FullSailClientSingleton.PauseTorrent(torrentFile.Hash);

		await Task.Delay(1000);

		await Refresh();
	});
	public ICommand DeleteTorrent => new Command<QBTFile>(async (torrentFile) =>
	{
		bool deleteTorrent = await AlertServiceSingleton.ShowConfirmationAsync("Confirm", "Are you SURE you want to delete this torrent?", "Yes", "No");

		if (deleteTorrent)
		{
			await FullSailClientSingleton.DeleteTorrent(torrentFile.Hash);

			await Task.Delay(1000);

			await Refresh();
		}
	});
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

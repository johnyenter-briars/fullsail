using FullSail.Shared.Models;
using FullSail.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FullSail.ViewModels;
internal class FilesInMediaStoreViewModel : BaseViewModel
{
	private string currentFolder = "media-root";

	public string CurrentFolder
	{
		get { return currentFolder; }
		set { SetProperty(ref currentFolder, value); }
	}
	public async Task Refresh(bool hardRefresh = false, string folderName = "media-root")
	{
		if (MediaFiles.Count > 0 && !hardRefresh)
		{
			return;
		}

		ToggleIsFetchingData();

		if (folderName == "media-root")
		{
			FolderPath.Clear();
			FolderPath.Push(new());
		}

		MediaFiles = await FullSailClientSingleton.GetMediaFilesInFolder(folderName);
		FilteredMediaFiles = MediaFiles;

		ToggleIsFetchingData();
	}
	private Stack<MediaFile> folderPath = new();

	public Stack<MediaFile> FolderPath
	{
		get { return folderPath; }
		set { SetProperty(ref folderPath, value); }
	}
	private List<MediaFile> mediaFiles = new();

	public List<MediaFile> MediaFiles
	{
		get { return mediaFiles; }
		set { SetProperty(ref mediaFiles, value); }
	}

	private List<MediaFile> filteredMediaFiles = new();

	public List<MediaFile> FilteredMediaFiles
	{
		get { return filteredMediaFiles; }
		set { SetProperty(ref filteredMediaFiles, value); }
	}

	public ICommand PerformSearch => new Command<string>((string query) =>
	{
		var filtered = mediaFiles.Where(f => f.Name.ToLower().Contains(query.ToLower())).ToList();

		FilteredMediaFiles = filtered;
	});
	public ICommand ClearSearchText => new Command(() =>
	{
		FilteredMediaFiles = mediaFiles;
	});
	public ICommand SendFile => new Command<MediaFile>(async (mediaFile) =>
	{
		if ((bool)(mediaFile?.IsFile))
		{
			var choice = await AlertServiceSingleton.ShowConfirmationAsync("Send to computer:", "", "Laptop", "Media-System");

			var destination = choice ? "laptop" : "mediaSystem";

			await FullSailClientSingleton.SendFile(mediaFile.Name, destination);
			AlertServiceSingleton.ShowAlert("Success", "File Sending Job Scheduled");
		}
	});
	public ICommand OpenFolder => new Command<MediaFile>(async (mediaFile) =>
	{
		if ((bool)!mediaFile?.IsFile)
		{
			await Refresh(true, mediaFile.FullPath);
			FolderPath.Push(mediaFile);
			CurrentFolder = mediaFile.ShortName;
		}
	});
	public ICommand MoveItem => new Command<MediaFile>(async (mediaFile) =>
	{
		await Shell.Current.GoToAsync($@"{nameof(MovingFilePage)}?{nameof(MovingFileViewModel.MovingFileFullName)}={Uri.EscapeDataString(mediaFile.Name)}");
	});
	public ICommand DeleteMediaFile => new Command<MediaFile>(async (mediaFile) =>
	{
		bool deleteFile = await AlertServiceSingleton.ShowConfirmationAsync("Confirm", "Are you SURE you want to delete this item from the Media-Store?", "Yes", "No");

		if (deleteFile)
		{
			await FullSailClientSingleton.DeleteFile(mediaFile.Name);

			await AlertServiceSingleton.ShowAlertAsync("Success", "Media-Store item deleted successfully");

			await Refresh(hardRefresh: true);
		}
	});
	public ICommand GoBack => new Command(async () =>
	{
		if (folderPath.Count > 1)
		{
			FolderPath.Pop();
			var folder = FolderPath.Peek();
			await Refresh(true, folder.FullPath);
			CurrentFolder = folder.ShortName ?? "media-root";
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

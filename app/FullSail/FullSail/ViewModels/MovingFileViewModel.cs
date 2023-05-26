using FullSail.Models;
using FullSail.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FullSail.ViewModels;

[QueryProperty(nameof(MovingFileFullName), nameof(MovingFileFullName))]
internal class MovingFileViewModel : BaseViewModel
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
	private string movingFileFullName = "";

	public string MovingFileFullName
	{
		get { return movingFileFullName; }
		set { SetProperty(ref movingFileFullName, value); }
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

	public ICommand MoveItemToHere => new Command<MediaFile>(async (mediaFile) =>
	{
		if (mediaFile.FullPath == movingFileFullName)
		{
			await AlertServiceSingleton.ShowAlertAsync("Not Allowed", "You can't request an item to move into itself!");
			await Shell.Current.GoToAsync("..");
			return;
		}

		bool moveFile = await AlertServiceSingleton.ShowConfirmationAsync("Confirm", $"Are you SURE you want to move item: {MovingFileFullName} to here?", "Yes", "No");

		if (moveFile)
		{
			var response = await FullSailClientSingleton.MoveFile(MovingFileFullName, mediaFile.Name);

			if (response.Message.ToLower().Contains("false"))
			{
				await AlertServiceSingleton.ShowAlertAsync("Failure", "Media-Store item was NOT moved successfully");
			}
			else
			{
				await AlertServiceSingleton.ShowAlertAsync("Success", "Media-Store item moved successfully");
			}

			await Shell.Current.GoToAsync("..");
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

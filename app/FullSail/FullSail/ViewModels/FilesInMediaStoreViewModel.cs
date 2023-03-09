using FullSail.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FullSail.ViewModels;
internal class FilesInMediaStoreViewModel : BaseViewModel
{
    public async Task Refresh(string folderName = "media-root")
    {
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
            await FullSailClientSingleton.SendFile(mediaFile.Name);
            AlertServiceSingleton.ShowAlert("Success", "File Sending Job Scheduled");
        }
    });
    public ICommand OpenFolder => new Command<MediaFile>(async (mediaFile) =>
    {
        if ((bool)!mediaFile?.IsFile)
        {
            await Refresh(mediaFile.FullPath);
            FolderPath.Push(mediaFile);
        }
    });
    public ICommand GoBack => new Command(async () =>
    {
        if (folderPath.Count > 1)
        {
            FolderPath.Pop();
            var folder = FolderPath.Peek();
            await Refresh(folder.FullPath);
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

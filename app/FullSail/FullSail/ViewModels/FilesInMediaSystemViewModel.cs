using FullSail.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FullSail.ViewModels;

public class FilesInMediaSystemViewModel : BaseViewModel
{
    public async Task Refresh()
    {
        MediaFiles = await FullSailClientSingleton.GetMediaFilesInMediaSystem();
        FilteredMediaFiles = MediaFiles;
    }
    private List<MediaFile> mediaFiles = new();

    public List<MediaFile> MediaFiles
    {
        get { return mediaFiles; }
        set { SetProperty(ref mediaFiles, value); }
    }

    private List<MediaFile> filteredMediaFiles = new()
    {
    };

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
    public ICommand DeleteFile => new Command<MediaFile>(async (mediaFile) =>
    {
        bool startTorrent = await AlertServiceSingleton.ShowConfirmationAsync("Confirm", "Are you SURE you want to delete this file from the Media-System?", "Yes", "No");

        if (startTorrent)
        {
            var response = await FullSailClientSingleton.DeleteFileInMediaSystem(mediaFile.ShortName);

            await AlertServiceSingleton.ShowAlertAsync("Success", "Media-System file deleted successfully");
        }
    });
    public ICommand PlayFile => new Command<MediaFile>(async (mediaFile) =>
    {
        await KodiClientSingleton.PlayFile(mediaFile.Name);
    });
}

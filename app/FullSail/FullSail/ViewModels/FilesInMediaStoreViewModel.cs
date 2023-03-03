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
    public async Task Refresh()
    {
        MediaFiles = await FullSailClientSingleton.GetMediaFilesInStore(false);
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
    public ICommand SendFile => new Command<MediaFile>(async (mediaFile) =>
    {
        await FullSailClientSingleton.SendFile(mediaFile.Name);
    });
}

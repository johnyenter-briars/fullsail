using FullSail.ViewModels;

namespace FullSail.Views;

public partial class FilesInMediaStorePage : ContentPage
{
    public FilesInMediaStorePage()
    {
        InitializeComponent();
        BindingContext = new FilesInMediaStoreViewModel();
    }
    private void ExecuteFileSelectedCommand(object sender, EventArgs e)
    {
        var foo = 10;
    }
}
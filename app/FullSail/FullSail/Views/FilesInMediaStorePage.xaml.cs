using FullSail.ViewModels;

namespace FullSail.Views;

public partial class FilesInMediaStorePage : ContentPage
{
    public FilesInMediaStorePage()
    {
        InitializeComponent();
        BindingContext = new FilesInMediaStoreViewModel();
    }
    private void ExecuteEventSelectedCommand(object sender, EventArgs e)
    {
        var foo = 10;
    }
}
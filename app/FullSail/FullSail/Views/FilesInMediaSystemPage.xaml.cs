using FullSail.ViewModels;

namespace FullSail.Views;

public partial class FilesInMediaSystemPage : ContentPage
{
    public FilesInMediaSystemPage()
    {
        InitializeComponent();

        BindingContext = new FilesInMediaSystemViewModel();
    }
    private void ExecuteFileSelectedCommand(object sender, EventArgs e)
    {
        var foo = 10;
    }
}
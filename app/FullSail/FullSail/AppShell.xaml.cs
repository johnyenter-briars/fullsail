using FullSail.Views;

namespace FullSail;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(MovingFilePage), typeof(MovingFilePage));
    }
}

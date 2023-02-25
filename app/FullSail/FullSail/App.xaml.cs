namespace FullSail;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        DependencyService.RegisterSingleton(new FullSailClient());

        MainPage = new AppShell();
    }
}

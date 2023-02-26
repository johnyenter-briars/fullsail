using FullSail.Managers;

namespace FullSail;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        DependencyService.RegisterSingleton(new FullSailClient()
            .UpdateSettings(PreferencesManager.GetHostname(),
                            PreferencesManager.GetPort(),
                            PreferencesManager.GetApiKey()
                            )
            );

        MainPage = new AppShell();
    }
}

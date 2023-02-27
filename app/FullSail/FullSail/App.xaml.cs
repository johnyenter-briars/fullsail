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

        DependencyService.RegisterSingleton(new KodiClient()
            .UpdateSettings(PreferencesManager.GetKodiHostname(),
                            PreferencesManager.GetKodiPort(),
                            PreferencesManager.GetKodiUsername(),
                            PreferencesManager.GetKodiPassword()
                            )
            );

        MainPage = new AppShell();
    }
}

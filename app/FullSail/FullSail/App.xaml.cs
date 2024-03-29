﻿using FullSail.Managers;
using FullSail.Shared;

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

		DependencyService.RegisterSingleton(new AlertService());

		MainPage = new AppShell();
	}
}

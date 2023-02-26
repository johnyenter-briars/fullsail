using FullSail.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FullSail.ViewModels
{
    internal class SettingsViewModel : BaseViewModel
    {
        private string buildVersion = Assembly.GetExecutingAssembly().GetName().ToString();
        public string BuildVersion { get => buildVersion; set => SetProperty(ref buildVersion, value); }

        private string apiKey = PreferencesManager.GetApiKey();
        public string ApiKey { get => apiKey; set => SetProperty(ref apiKey, value); }

        private string hostname = PreferencesManager.GetHostname()?.ToString();
        public string HostName { get => hostname; set => SetProperty(ref hostname, value); }

        private string port = PreferencesManager.GetPort().ToString();
        public string Port { get => port; set => SetProperty(ref port, value); }

        private string kodiHostname = PreferencesManager.GetKodiHostname()?.ToString();
        public string KodiHostname { get => kodiHostname; set => SetProperty(ref kodiHostname, value); }

        private string kodiPort = PreferencesManager.GetKodiPort().ToString();
        public string KodiPort { get => kodiPort; set => SetProperty(ref kodiPort, value); }

        private string kodiUsername = PreferencesManager.GetKodiUsername()?.ToString();
        public string KodiUsername { get => kodiUsername; set => SetProperty(ref kodiUsername, value); }

        private string kodiPassword = PreferencesManager.GetKodiPassword()?.ToString();
        public string KodiPassword { get => kodiPassword; set => SetProperty(ref kodiPassword, value); }

        private Command saveChangesCommand;
        public ICommand SaveChangesCommand
        {
            get
            {
                if (saveChangesCommand == null)
                {
                    saveChangesCommand = new Command(SaveChanges);
                }

                return saveChangesCommand;
            }
        }
        private void SaveChanges()
        {
            if (int.TryParse(port, out int p) && int.TryParse(kodiPort, out int n))
            {
                PreferencesManager.SetSettings(hostname,
                                                p,
                                                apiKey,
                                                kodiHostname,
                                                n,
                                                kodiUsername,
                                                kodiPassword);

                FullSailClientSingleton.UpdateSettings(PreferencesManager.GetHostname(),
                                            PreferencesManager.GetPort(),
                                            PreferencesManager.GetApiKey()
                                            );
            }
        }

    }
}

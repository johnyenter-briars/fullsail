using System;
using System.Collections.Generic;
using System.Text;

namespace FullSail.Managers
{
    internal static class PreferencesManager
    {
        private static readonly string apiKeyTag = "apiKey";
        private static readonly string hostnameTag = "hostnameKey";
        private static readonly string portTag = "portKey";
        private static readonly string kodiHostnameTag = "kodiHostnameKey";
        private static readonly string kodiPortTag = "kodiPortKey";
        private static readonly string kodiUsernameTag = "kodiUsernameKey";
        private static readonly string kodiPasswordTag = "kodiPasswordKey";
        public static string GetApiKey()
        {
            return Preferences.Get(apiKeyTag, null);
        }
        public static string GetHostname()
        {
            return Preferences.Get(hostnameTag, null);
        }
        public static int GetPort()
        {
            return Preferences.Get(portTag, 0);
        }
        public static string GetKodiHostname()
        {
            return Preferences.Get(kodiHostnameTag, null);
        }
        public static int GetKodiPort()
        {
            return Preferences.Get(kodiPortTag, 0);
        }
        public static string GetKodiUsername()
        {
            return Preferences.Get(kodiUsernameTag, null);
        }
        public static string GetKodiPassword()
        {
            return Preferences.Get(kodiPasswordTag, null);
        }
        public static void SetSettings(string hostname, int port, string apiKey, string kodiHostname, int kodiPort, string kodiUsername, string kodiPassword)
        {
            Preferences.Set(hostnameTag, hostname);
            Preferences.Set(apiKeyTag, apiKey);
            Preferences.Set(portTag, port);
            Preferences.Set(kodiHostnameTag, kodiHostname);
            Preferences.Set(kodiPortTag, kodiPort);
            Preferences.Set(kodiUsernameTag, kodiUsername);
            Preferences.Set(kodiPasswordTag, kodiPassword);
        }
    }
}

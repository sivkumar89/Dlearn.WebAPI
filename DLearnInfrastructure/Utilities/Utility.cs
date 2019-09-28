using System.Configuration;

namespace DLearnInfrastructure.Utilities
{
    public class Utility
    {
        public static string GetAppSettings(string key)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                return ConfigurationManager.AppSettings[key];
            }
            return null;
        }
    }
}

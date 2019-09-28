using System.Configuration;

namespace DLearnRepositories.Utilities
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

        public static string GetConnectionString(string key)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                return ConfigurationManager.ConnectionStrings[key].ConnectionString;
            }
            return null;
        }
    }
}

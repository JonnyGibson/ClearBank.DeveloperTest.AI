using System.Configuration;

namespace ClearBank.DeveloperTest.Services
{
    public class ConfigurationService : IConfigurationService
    {
        public string GetDataStoreType()
        {
            return ConfigurationManager.AppSettings["DataStoreType"];
        }
    }
} 
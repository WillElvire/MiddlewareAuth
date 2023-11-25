using System;
namespace MomoApi.Utils
{
    public class Configuration
    {
        ConfigurationBuilder configurationBuilder;
        IConfigurationRoot Config;

        public Configuration()
        {
            configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("appsettings.json");

        }

        public string get(string keyName)
        {
            Config = configurationBuilder.Build();
            return Config[keyName];

        }
    }
}


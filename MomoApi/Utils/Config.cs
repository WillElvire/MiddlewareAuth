using System;
namespace MomoApi.Utils
{
	public class Config
	{
        public IConfigurationRoot configuration = null;
        public Config()
        {

            configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory()) // Spécifie le chemin du répertoire actuel
        .AddJsonFile("appsettings.json") // Charge le fichier appsettings.json
        .Build();

        }
    }
}


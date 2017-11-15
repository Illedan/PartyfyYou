using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace Partify.Storage.Server.Configuration
{
    public class PartifyConfiguration : IConfiguration
    {
        private readonly ConfigurationModel m_configurationModel;

        public PartifyConfiguration()
        {
            m_configurationModel = GetDatabaseConnectionString().Result;
        }

        public string ConnectionString { get => m_configurationModel.DBConnectionString; }

        private async Task<ConfigurationModel> GetDatabaseConnectionString()
        {
            using (StreamReader reader = new StreamReader(GetPath()))
            {
                var jsonString = await reader.ReadToEndAsync();

                var keyModelResult = JsonConvert.DeserializeObject<ConfigurationModel>(jsonString);
                return keyModelResult;
            }
            //TODO: What path to return when running in a docker container?
            string GetPath()
            {
                var codeBase = Assembly.GetExecutingAssembly().CodeBase;
                var uri = new UriBuilder(codeBase);
                var path = Uri.UnescapeDataString(uri.Path);
                var pathToExecutingFolder = Path.GetDirectoryName(path);
                var configFilePath = Path.GetFullPath(Path.Combine(pathToExecutingFolder, @"..\..\..\", "Configuration.json"));
                return configFilePath;
            }
        }
        
    }
}

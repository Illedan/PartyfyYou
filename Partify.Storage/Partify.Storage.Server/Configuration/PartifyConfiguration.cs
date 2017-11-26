using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace Partify.Storage.Server.Configuration
{
    public class PartifyConfiguration : IConfiguration
    {
        public PartifyConfiguration(string dbConnectionString)
        {
            ConnectionString = dbConnectionString;
        }

        public string ConnectionString { get; private set; }
        
    }
}

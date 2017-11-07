using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpotifyListner.Web.Models;

namespace SpotifyListner.Web.Services
{
    public class KeyService : IKeyService
    {
        public async Task<KeyModel> GetKeys()
        {
           
            using (StreamReader reader = new StreamReader(GetPath()))
            {
                var jsonString = await reader.ReadToEndAsync();

                var keyModelResult = JsonConvert.DeserializeObject<KeyModel>(jsonString);
                return keyModelResult;
            }
            
            string GetPath()
            {
                var pathEnding = ConfigurationManager.AppSettings["KeysPathEnding"];
                var codeBase = Assembly.GetExecutingAssembly().CodeBase;
                var uri = new UriBuilder(codeBase);
                var path = Uri.UnescapeDataString(uri.Path);
                var pathToExecutingFolder = Path.GetDirectoryName(path);

                return pathToExecutingFolder + pathEnding;
            }
        }
    }
}
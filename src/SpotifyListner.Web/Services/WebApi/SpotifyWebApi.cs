using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpotifyListner.Web.Models;

namespace SpotifyListner.Web.Services.WebApi
{
    public class SpotifyWebApi : ISpotifyWebApi
    {
        public async Task<string> RefreshToken(string token)
        {
            var newToken = await GetAsync<string>("TOKEN URL", token);
            throw new Exception("REFRESH is CRAPSKY");
        }

        public async Task<SpotifyContent> GetPlayingSong(string token)
        {
            var content = await GetAsync<SpotifyContent>("https://api.spotify.com/v1/me/player/currently-playing", token);
            return content;
        }

        public async Task<SpotifyUser> GetUser(string token)
        {
            SpotifyUser user = await GetAsync<SpotifyUser>("https://api.spotify.com/v1/me", token);
            
           
            return user;
        }

        private static async Task<T> GetAsync<T>(string url, string token)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage responeMessage;

            
                responeMessage = await httpClient.GetAsync(url);
           
        
           
            var result = await responeMessage.Content.ReadAsStringAsync();
            var res = JsonConvert.DeserializeObject<T>(result);
            return res;
        }
    }
}
using Newtonsoft.Json;
using SpotifyListner.Web.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyListner.Web.Services
{
    public class Authorization : IAuthorization
    {
        private WebClient webClient;
        private string redirectUri = "http://localhost:1337/"; 
        private string spotifyTokenUri = "https://accounts.spotify.com/api/token";
        private string clientId = "dfce289f6499436bbd1d60033ac14957";
        private string clientSecret = "";

        public string GetNewToken(string originalToken)
        {
            webClient = new WebClient();
            var postparams = new NameValueCollection();
            postparams.Add("grant_type", "client_credentials");

            var authHeader = Convert.ToBase64String(Encoding.Default.GetBytes($"{clientId}:{clientSecret}"));
            webClient.Headers.Add(HttpRequestHeader.Authorization, "Basic " + authHeader);

            var tokenResponse = webClient.UploadValues(spotifyTokenUri, postparams);

            var textResponse = Encoding.UTF8.GetString(tokenResponse);
            var tokenResult = JsonConvert.DeserializeObject<TokenResponse>(textResponse);
            return tokenResult.access_token;
        }
    }
}

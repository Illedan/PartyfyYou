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
        private string redirectUri = "http://localhost:1337/callback/";
        private string spotifyTokenUri = "https://accounts.spotify.com/api/token";
        private string clientId = "dfce289f6499436bbd1d60033ac14957";
        private string clientSecret = "";

        public TokenResponse GetToken(string code)
        {
            var webClient = SetupWebClient();

            var postparams = new NameValueCollection();
            postparams.Add("grant_type", "authorization_code");
            postparams.Add("code",code);
            postparams.Add("redirect_uri", redirectUri);
            
            var byteResponse = webClient.UploadValues(spotifyTokenUri, postparams);
            var textResponse = Encoding.UTF8.GetString(byteResponse);
            var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(textResponse);
            return tokenResponse;
        }

        public RefreshTokenResponse RefreshToken(string refreshToken)
        {
            var webClient = SetupWebClient();

            var postparams = new NameValueCollection();
            postparams.Add("grant_type", "refresh_token");
            postparams.Add("refresh_token", refreshToken);
            
            var byteResponse = webClient.UploadValues(spotifyTokenUri, postparams);
            var textResponse = Encoding.UTF8.GetString(byteResponse);
            var tokenResponse = JsonConvert.DeserializeObject<RefreshTokenResponse>(textResponse);

            return tokenResponse;
        }

        private WebClient SetupWebClient()
        {
            var webClient = new WebClient();
            var authHeader = Convert.ToBase64String(Encoding.Default.GetBytes($"{clientId}:{clientSecret}"));
            webClient.Headers.Add(HttpRequestHeader.Authorization, "Basic " + authHeader);
            return webClient;
        }
    }
}

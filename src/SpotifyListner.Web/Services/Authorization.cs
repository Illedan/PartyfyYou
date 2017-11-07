using Newtonsoft.Json;
using SpotifyListner.Web.Models;
using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyListner.Web.Services
{
    public class Authorization : IAuthorization
    {
        private readonly IKeyService m_keyService;
        private readonly string redirectUri;
        private readonly string spotifyTokenUri = "https://accounts.spotify.com/api/token";

        public Authorization(IKeyService keyService)
        {
            m_keyService = keyService;
            redirectUri = ConfigurationManager.AppSettings["RedirectUri"];
        }

        public async Task<TokenResponse> GetToken(string code)
        {
            var webClient = await SetupWebClient();

            var postparams = new NameValueCollection();
            postparams.Add("grant_type", "authorization_code");
            postparams.Add("code",code);
            postparams.Add("redirect_uri", redirectUri);
            
            var byteResponse = webClient.UploadValues(spotifyTokenUri, postparams);
            var textResponse = Encoding.UTF8.GetString(byteResponse);
            var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(textResponse);
            return tokenResponse;
        }

        public async Task<RefreshTokenResponse> RefreshToken(string refreshToken)
        {
            var webClient = await SetupWebClient();

            var postparams = new NameValueCollection();
            postparams.Add("grant_type", "refresh_token");
            postparams.Add("refresh_token", refreshToken);
            
            var byteResponse =  webClient.UploadValues(spotifyTokenUri, postparams);
            var textResponse = Encoding.UTF8.GetString(byteResponse);
            var tokenResponse = JsonConvert.DeserializeObject<RefreshTokenResponse>(textResponse);

            return tokenResponse;
        }

        private async Task<WebClient> SetupWebClient()
        {
            var keysResult = await m_keyService.GetKeys();

            var webClient = new WebClient();

            var authHeader = Convert.ToBase64String(Encoding.Default.GetBytes($"{keysResult.SpotifyClientId}:{keysResult.SpotifyClientSecret}"));
            webClient.Headers.Add(HttpRequestHeader.Authorization, "Basic " + authHeader);
            return webClient;
        }
    }
}

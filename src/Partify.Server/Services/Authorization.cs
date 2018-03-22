using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Partify.Server.Configuration;
using Partify.Server.Models;

namespace Partify.Server.Services
{
    public class Authorization : IAuthorization
    {
        private readonly IConfiguration m_configuration;
        private readonly string spotifyTokenUri = "https://accounts.spotify.com/api/token";

        public Authorization(IConfiguration configuration)
        {
            m_configuration = configuration;
        }

        public async Task<TokenResponse> GetToken(string code)
        {
            var webClient = await SetupWebClient();

            var postparams = new NameValueCollection();
            postparams.Add("grant_type", "authorization_code");
            postparams.Add("code",code);
            postparams.Add("redirect_uri", m_configuration.RedirectUri);
            
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
            var webClient = new WebClient();

            var authHeader = Convert.ToBase64String(Encoding.Default.GetBytes($"{m_configuration.SpotifyClientId}:{m_configuration.SpotifyClientSecret}"));
            webClient.Headers.Add(HttpRequestHeader.Authorization, "Basic " + authHeader);
            return webClient;
        }
    }
}

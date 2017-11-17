using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AppAuthenticationServer.Configuration;
using AppAuthenticationServer.Model;
using Newtonsoft.Json;

namespace AppAuthenticationServer.Services
{
    /// <summary>
    /// Following:
    /// https://developer.spotify.com/web-api/authorization-guide/
    /// </summary>
    public class SpotifyAuthenticator
    {
        readonly ISpotifyAuthConfig appConfig;
        readonly AuthService authService;

        readonly string callbackURL;

        public SpotifyAuthenticator(ISpotifyAuthConfig appConfig, AuthService authService)
        {
            this.appConfig = appConfig;
            this.authService = authService;

            // TODO: Må være dynamisk basert på host
            callbackURL = "http://localhost:5000/activate/callback/";
            const string Scopes = "user-read-currently-playing user-read-playback-state";
            SpotifyAuthenticationURL = $"https://accounts.spotify.com/authorize?client_id={appConfig.SpotifyClientId}&redirect_uri={callbackURL}&scope={Scopes}&response_type=code";
        }

        public string SpotifyAuthenticationURL { get; }

        public async Task GetSpotifyAccessToken(string spotifyAuthCode) {
            using(var httpClient = new HttpClient()) {
                var authHeader = Convert.ToBase64String(Encoding.Default.GetBytes($"{appConfig.SpotifyClientId}:{appConfig.SpotifyClientSecret}"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeader);
                var postParams = new Dictionary<string, string>
                {
                    { "grant_type", "authorization_code" },
                    { "code", spotifyAuthCode },
                    { "redirect_uri", callbackURL }
                };

                using (var postContent = new FormUrlEncodedContent(postParams)) {
                    using (HttpResponseMessage response = await httpClient.PostAsync("https://accounts.spotify.com/api/token", postContent))
                    {
                        response.EnsureSuccessStatusCode();
                        using (HttpContent content = response.Content)
                        {
                            string textResponse = await content.ReadAsStringAsync();
                            var spotifySession = JsonConvert.DeserializeObject<SpotifySession>(textResponse);

                        }
                    }    
                }
            }
        }
    }
}

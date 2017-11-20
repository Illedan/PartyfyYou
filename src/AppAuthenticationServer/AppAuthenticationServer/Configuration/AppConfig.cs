using System;
namespace AppAuthenticationServer.Configuration
{
    public class AppConfig : ISpotifyAuthConfig
    {
        public string SpotifyClientId { get; set; }
        public string SpotifyClientSecret { get; set; }
    }
}

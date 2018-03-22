namespace Partify.Server.Configuration
{
    public class Configuration : IConfiguration
    {
        public Configuration(string spotifyClientId, string spotifyClientSecret, string youTubeServiceId, string redirectUri)
        {
            SpotifyClientId = spotifyClientId;
            SpotifyClientSecret = spotifyClientSecret;
            YouTubeServiceId = youTubeServiceId;
            RedirectUri = redirectUri;
        }
        public string SpotifyClientId { get;  }
        public string SpotifyClientSecret { get;  }
        public string YouTubeServiceId { get;  }
        public string RedirectUri { get;  }
    }
}
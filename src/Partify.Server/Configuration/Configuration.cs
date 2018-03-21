namespace Partify.Server.Configuration
{
    public class Configuration : IConfiguration
    {
        public Configuration(string spotifyClientId, string spotifyClientSecret, string youTubeServiceId)
        {
            SpotifyClientId = spotifyClientId;
            SpotifyClientSecret = spotifyClientSecret;
            YouTubeServiceId = youTubeServiceId;
        }
        public string SpotifyClientId { get; set; }
        public string SpotifyClientSecret { get; set; }
        public string YouTubeServiceId { get; set; }
    }
}
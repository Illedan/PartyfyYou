namespace Partify.Server.Configuration
{
    public interface IConfiguration
    {
        string SpotifyClientId { get; set; }
        string SpotifyClientSecret { get; set; }
        string YouTubeServiceId { get; set; }
    }
}
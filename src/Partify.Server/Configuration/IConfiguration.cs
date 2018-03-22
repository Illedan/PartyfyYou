namespace Partify.Server.Configuration
{
    public interface IConfiguration
    {
        string SpotifyClientId { get; }
        string SpotifyClientSecret { get; }
        string YouTubeServiceId { get; }
        string RedirectUri { get; }
    }
}
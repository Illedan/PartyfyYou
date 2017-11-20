using System;
namespace AppAuthenticationServer.Configuration
{
    public interface ISpotifyAuthConfig
    {
        string SpotifyClientId { get; }
        string SpotifyClientSecret { get; }
    }
}

using SpotifyListner.Web.Services;

namespace SpotifyListner.Web
{
    public static class StaticContainer
    {
        static StaticContainer()
        {
            YouTubeGoogleService = new YouTubeGoogleService();
            SpotifyService = new SpotifyService(YouTubeGoogleService);
        }

        public static YouTubeGoogleService YouTubeGoogleService{ get; }
        public static ISpotifyService SpotifyService { get; }
    }
}
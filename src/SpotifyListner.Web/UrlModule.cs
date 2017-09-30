using Nancy;

namespace SpotifyListner.Web
{
    public class UrlModule : NancyModule
    {
        public UrlModule()
        {
            var youTubeGoogleService = StaticContainer.YouTubeGoogleService;
            var spotifyService = StaticContainer.SpotifyService;

            Get["/url", true] = async (parameters, ct) =>
            {
                var song = await spotifyService.GetCurrentSong();
                return await youTubeGoogleService.FetchUrl(song);
            };

            Get["/pause/{id}", true] = (parameters, ct) => spotifyService.PauseSong(parameters["id"]);
        }
    }
}

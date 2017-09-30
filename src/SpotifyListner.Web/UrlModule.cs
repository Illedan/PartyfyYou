using Nancy;
using SpotifyListner.Web.Services;

namespace SpotifyListner.Web
{
    public class UrlModule : NancyModule
    {
        public UrlModule(IYouTubeGoogleService youTubeGoogleService, ISpotifyService spotifyService)
        {
            Get["/url", true] = async (parameters, ct) =>
            {
                var song = await spotifyService.GetCurrentSong();
                return await youTubeGoogleService.FetchUrl(song);
            };

            Get["/pause/{id}", true] = async (parameters, ct) => await spotifyService.PauseSong(parameters["id"]);
        }
    }
}

using Nancy;
using Nancy.Extensions;
using Nancy.IO;
using SpotifyListner.Web.Services;

namespace SpotifyListner.Web
{
    public class UrlModule : NancyModule
    {
        public UrlModule(IYouTubeGoogleService youTubeGoogleService, ISpotifyService spotifyService)
        {
            Get["/url", true] = async (parameters, ct) =>
            {
                string token = this.Request.Query["token"];
                string mode = this.Request.Query["mode"];
                var song = (await spotifyService.GetCurrentSong(token));
                return await youTubeGoogleService.FetchUrl(song, mode);
            };

            Get["/id", true] = async (parameters, ct) =>
            {
                string token = this.Request.Query["token"];
                var song = (await spotifyService.GetCurrentSong(token));
                if (song != null && song.item != null)
                {
                    return song.item.id;
                }
                return null;
            };
            //Get["/pause/{id}", true] = async (parameters, ct) => await spotifyService.PauseSong(parameters["id"]);

            Get["/join/asd/", true] = async (parameters, ct) =>
            {
                string token = this.Request.Query["token"];

                var song =  (await spotifyService.GetCurrentSong(token));

                return song.item.id;
            };

        }
    }
}

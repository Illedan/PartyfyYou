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
                var song = await spotifyService.GetCurrentSong();
                return await youTubeGoogleService.FetchUrl(song);
            };

            Get["/pause/{id}", true] = async (parameters, ct) => await spotifyService.PauseSong(parameters["id"]);
            //Post["/join/getsongwithtoken", true] = async (parameters, ct) =>
            //    {
            //        var body = this.Request.Body;
            //        int length = (int)body.Length; // this is a dynamic variable
            //        byte[] data = new byte[length];
            //        body.Read(data, 0, length);
            //        var a =(System.Text.Encoding.Default.GetString(data));

            //        return await spotifyService.GetCurrentSong(a);
            //    };
            Get["/join/asd/", true] = async (parameters, ct) =>
            {
                // var token = parameters["token"];
                string token = this.Request.Query["token"];

                return await spotifyService.GetCurrentSong(token);
            };

        }
    }
}

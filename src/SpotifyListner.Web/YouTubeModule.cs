using Nancy;
using Nancy.ModelBinding;
using SpotifyListner.Web.Models;
using SpotifyListner.Web.Services;

namespace SpotifyListner.Web
{
    public class YouTubeModule : NancyModule
    {
        public YouTubeModule()
        {
            Get["/youtubeurl"] = param =>
            {
                var data = this.Bind<SpotifySong>();
                //TODO: add container.
                return new YouTubeGoogleService().FetchUrl(data);
            };
        }
    }
}
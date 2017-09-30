using System.Threading.Tasks;
using Google.Apis.Services;
using SpotifyListner.Web.Models;
using System.Linq;
using Google.Apis.YouTube.v3;

namespace SpotifyListner.Web.Services
{
    public class YouTubeGoogleService : IYouTubeGoogleService
    {
        public async Task<string> FetchUrl(SpotifySong spotifySong)
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyDDzSJAO2jU6Pl5IFtOLnN3rmhtq4jVwBQ",// "YouTube-APIkey => https://console.developers.google.com/apis/credentials/",
                ApplicationName = "PartyfyYou"
            });

            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = spotifySong.Song + " " + spotifySong.Artist; 
            searchListRequest.MaxResults = 1;

            var searchListResponse = await searchListRequest.ExecuteAsync();


            var song = searchListResponse.Items.FirstOrDefault(s => s.Id.Kind.Equals("youtube#video"));
            var songId = song?.Id?.VideoId;
            if (song == null)
            {
                //TODO: pick random videos if not found.
                //https://www.youtube.com/watch?v=UcRtFYAz2Yo
                songId = "UcRtFYAz2Yo";
            }

            var url = "https://www.youtube.com/embed/" + songId + "?autoplay=1";
            return url;
        }
    }
}
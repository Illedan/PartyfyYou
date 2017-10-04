using System;
using System.Configuration;
using System.Threading.Tasks;
using Google.Apis.Services;
using SpotifyListner.Web.Models;
using System.Linq;
using System.Xml;
using Google.Apis.YouTube.v3;

namespace SpotifyListner.Web.Services
{
    public class YouTubeGoogleService : IYouTubeGoogleService
    {
        private readonly YouTubeService youtubeService;
        public YouTubeGoogleService()
        {
            youtubeService = new YouTubeService(new BaseClientService.Initializer
            {
                ApiKey = ConfigurationManager.AppSettings["YouTubeServiceApiKey"],// "YouTube-APIkey => https://console.developers.google.com/apis/credentials/",
                ApplicationName = "PartyfyYou"
            });
        }

        public async Task<string> FetchUrl(SpotifyContent spotifySong)
        {
            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = spotifySong.item.name + " " + spotifySong.item.artists.First().name; 
            searchListRequest.MaxResults = 1;

            var searchListResponse = await searchListRequest.ExecuteAsync();

            var song = searchListResponse.Items.FirstOrDefault(s => s.Id.Kind.Equals("youtube#video"));
            var songId = song?.Id?.VideoId;
            if (song == null)
            {
                //TODO: pick random videos if not found.
                return "15_Y3_eRfOU&t";
            }

            return songId;
        }

        public async Task<TimeSpan> GetSongLength(string id)
        {
            var videoRequest = youtubeService.Videos.List("id, contentDetails");
            videoRequest.Id = id;
            var video = await videoRequest.ExecuteAsync();
            var videoDuration = XmlConvert.ToTimeSpan(video.Items.FirstOrDefault()?.ContentDetails.Duration);
            return videoDuration;
        }
    }
}
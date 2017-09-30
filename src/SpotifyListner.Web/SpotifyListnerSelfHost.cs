using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Services;

using Google.Apis.YouTube.v3;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web.Enums;
using System.IO;
using System.Configuration;

namespace SpotifyListner.Web
{
    public class SpotifyListnerSelfHost
    {
        private ServiceTimer ServiceTimer;
        private int Interval = 1;
        private SpotifyWebAPI _spotify;
        private string previous = "";
        private string apiKey;
        public SpotifyListnerSelfHost(string listeningInfo)
        {
            Console.WriteLine($"SpotifyListner listening on: {listeningInfo}");
            apiKey = ConfigurationManager.AppSettings["YouTubeServiceApiKey"];
            _spotify = new SpotifyWebAPI();
            RunAuthentication();
        }

        public bool Start()
        {
            ServiceTimer = new ServiceTimer
            {
                Enabled = true,
                Interval = 1000 * Interval
            };
            ServiceTimer.Elapsed += DoStuff;
            return true;
        }

        protected async void DoStuff(object sender, System.Timers.ElapsedEventArgs e)
        {

            try
            {
                var track = _spotify.GetPlayingTrack();
                if (previous != track.Item.Name)
                {
                    
                    previous = track.Item.Name;
                    var youtubeService = new YouTubeService(new BaseClientService.Initializer()
                    {
                        ApiKey = apiKey,// "YouTube-APIkey => https://console.developers.google.com/apis/credentials/",
                        ApplicationName = this.GetType().ToString()
                    });

                    var searchListRequest = youtubeService.Search.List("snippet");
                    searchListRequest.Q = previous + " " + track.Item.Artists.First().Name; // Replace with your search term.
                    searchListRequest.MaxResults = 1;

                    // Call the search.list method to retrieve results matching the specified query term.
                    var searchListResponse = await searchListRequest.ExecuteAsync();
                    var song = searchListResponse.Items.First(s => s.Id.Kind.Equals("youtube#video"));
                    var url = "https://www.youtube.com/embed/" + song.Id.VideoId + "?autoplay=1";

                    if (File.Exists("c:\\videoLink.txt"))
                    {
                        string oldurl = File.ReadAllText("c:\\videoLink.txt");
                        if (!url.Equals(oldurl))
                        {
                            StreamWriter file = new StreamWriter("c:\\videoLink.txt");
                            file.Write(url);

                            file.Close();
                        }

                    }
                    else
                    {
                        StreamWriter file = new StreamWriter("c:\\videoLink.txt");
                        file.Write(url);

                        file.Close();
                    }

                    //Åpne chrome og gå til http://localhost:1337/ :)


                }
            }
            catch (Exception exception)
            {
               var x = exception.Message;
            }

        }



        public bool Stop()
        {

            ServiceTimer.Elapsed -= DoStuff;
            return false;
        }

        private async void RunAuthentication()
        {
            var webApiFactory = new WebAPIFactory(
                "http://localhost",
                8000,
                "26d287105e31491889f3cd293d85bfea",
                Scope.UserReadPrivate | Scope.UserReadEmail | Scope.PlaylistReadPrivate | Scope.UserLibraryRead |
                Scope.UserReadPrivate | Scope.UserFollowRead | Scope.UserReadBirthdate | Scope.UserTopRead | Scope.PlaylistReadCollaborative |
                Scope.UserReadRecentlyPlayed | Scope.UserReadPlaybackState | Scope.UserModifyPlaybackState);

            try
            {
                _spotify = await webApiFactory.GetWebApi();
            }
            catch (Exception ex)
            {
                var exe = ex;
            }
        }
    }
}

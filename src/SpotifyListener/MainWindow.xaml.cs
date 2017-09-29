using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web.Enums;

namespace SpotifyListener
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            _spotify = new SpotifyWebAPI();
            RunAuthentication();
            Runner();
        }

        private SpotifyWebAPI _spotify;
        private string previous = "";

        private async void Runner()
        {
            while (true)
            {
                await Task.Delay(1000);
                try
                {
                    var track = _spotify.GetPlayingTrack();
                    if (previous != track.Item.Name)
                    {
                        YOLOSHIT.Text = track.Item.Name;
                        previous = track.Item.Name;
                        var youtubeService = new YouTubeService(new BaseClientService.Initializer()
                        {
                            ApiKey = "AIzaSyDDzSJAO2jU6Pl5IFtOLnN3rmhtq4jVwBQ",// "YouTube-APIkey => https://console.developers.google.com/apis/credentials/",
                            ApplicationName = this.GetType().ToString()
                        });

                        var searchListRequest = youtubeService.Search.List("snippet");
                        searchListRequest.Q = previous + " " + track.Item.Artists.First().Name; // Replace with your search term.
                        searchListRequest.MaxResults = 1;

                        // Call the search.list method to retrieve results matching the specified query term.
                        var searchListResponse = await searchListRequest.ExecuteAsync();
                        var song = searchListResponse.Items.First(s => s.Id.Kind.Equals("youtube#video"));
                        var url = "https://www.youtube.com/embed/"+ song.Id.VideoId + "?autoplay=1";

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

                        webBrowser.Navigate("https://www.youtube.com/watch?v=" + song.Id.VideoId); 
                    }
                }
                catch (Exception exception)
                {
                    YOLOSHIT.Text = exception.Message;
                }   
            }
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
                MessageBox.Show(ex.Message);
            }
        }
    }
}

using System;
using System.Threading.Tasks;
using SpotifyListner.Web.Models;
using SpotifyListner.Web.Services.WebApi;

namespace SpotifyListner.Web.Services
{
    public class SpotifyService : ISpotifyService
    {
        private readonly IYouTubeGoogleService m_youTubeGoogleService;
        private readonly ISpotifyWebApi m_spotifyWebApi;

        public SpotifyService(IYouTubeGoogleService youTubeGoogleService, ISpotifyWebApi spotifyWebApi)
        {
            m_youTubeGoogleService = youTubeGoogleService;
            m_spotifyWebApi = spotifyWebApi;
        }

        public async Task PauseSong(string id, string token)
        {
            await Task.Delay(1);
            throw new Exception("TODO");

            //var currentSong = await GetCurrentSong();
            //var videoDuration = await m_youTubeGoogleService.GetSongLength(id);

            //// Get youtube video info

            //var durationDiffrence = videoDuration - new TimeSpan(0, 0, 0, 0, currentSong.Time) + TimeSpan.FromSeconds(2);

            //// If diffrence is more than 0 ms
            //if (durationDiffrence.TotalMilliseconds > 0)
            //{
            //    // Pause spotify untill youtube and spotify is synchronized.
            //    m_spotifyWebApi.PausePlayback();
            //    await Task.Delay(durationDiffrence);
            //    m_spotifyWebApi.ResumePlayback();
            //}
            //else
            //{
            //    // Skip spotify to match youtube video.
            //    var progress = m_spotifyWebApi.GetPlayback().ProgressMs;
            //    progress = progress - int.Parse(durationDiffrence.TotalMilliseconds.ToString());
            //    m_spotifyWebApi.SeekPlayback(progress);
            //}
        }

        public Task<SpotifyContent> GetCurrentSong(string token)
        {
            return m_spotifyWebApi.GetPlayingSong(token);
        }
    }
}
using System;
using System.Threading.Tasks;
using Partify.Server.Models;
using Partify.Server.Services.WebApi;

namespace Partify.Server.Services
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

        public async Task<SpotifyContent> GetCurrentSong(string token)
        { 
            var spotifyContent = await m_spotifyWebApi.GetPlayingSong(token);
            return spotifyContent;
        }

        public async Task<SpotifyUser> GetUser(string token)
        {
            var user = await m_spotifyWebApi.GetUser(token);
            return user;
        }
    }
}
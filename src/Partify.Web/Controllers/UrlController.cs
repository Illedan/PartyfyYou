using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Partify.Server.Models;
using Partify.Server.Services;

namespace Partify.Web.Controllers
{
    [Route("/")]
    public class UrlController : Controller
    {
        private readonly IYouTubeGoogleService m_youTubeGoogleService;
        private readonly ISpotifyService m_spotifyService;
        private readonly IPartifyStorageService m_partifyStorageService;

        public UrlController(IYouTubeGoogleService youTubeGoogleService, ISpotifyService spotifyService, IPartifyStorageService partifyStorageService)
        {
            m_youTubeGoogleService = youTubeGoogleService;
            m_spotifyService = spotifyService;
            m_partifyStorageService = partifyStorageService;
        }

        [HttpGet("url")]
        public async Task<string> Get()
        {
            string token = this.Request.Query["token"];
            string mode = this.Request.Query["mode"];
            string userId = this.Request.Query["userId"];
            string modeId = this.Request.Query["modeId"];
            var song = (await m_spotifyService.GetCurrentSong(token));
            var songId = song?.item?.id;
            string videoId = null;

            if (!string.IsNullOrEmpty(songId) && !string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(modeId))
            {
                var userIdDidParse = Guid.TryParse(userId, out var userIdGuid);
                var modeIdDidParse = Guid.TryParse(modeId, out var modeIdGuid);
                var suggestion = await m_partifyStorageService.GetSuggestion(songId, modeIdGuid, userIdGuid);
                if (suggestion != null)
                {
                    return suggestion.YoutubeVideoId;
                }
                videoId = await m_youTubeGoogleService.FetchUrl(song, mode);
                if (!string.IsNullOrEmpty(videoId))
                {

                    await m_partifyStorageService.AddSuggestion(videoId, songId, modeIdGuid, userIdGuid);
                }
            }


            return videoId;
        }

        [HttpGet("id")]
        public async Task<string> Get2()
        {
            string token = this.Request.Query["token"];
            var song = (await m_spotifyService.GetCurrentSong(token));
            if (song != null && song.item != null)
            {
                return song.item.id;
            }
            return null;
        }

        [HttpGet("search")]
        public async Task<List<SpotifySearchResult>> Get3()
        {
            string token = this.Request.Query["token"];
            string mode = this.Request.Query["mode"];
            var song = (await m_spotifyService.GetCurrentSong(token));
            return await m_youTubeGoogleService.GetSearchResults(song, mode, 4);
        }
    }
}
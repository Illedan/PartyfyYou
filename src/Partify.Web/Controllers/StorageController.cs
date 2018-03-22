using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Partify.Server.Models;
using Partify.Server.Services;

namespace Partify.Web.Controllers
{
    [Route("/")]
    public class StorageController : Controller
    {
        private readonly IYouTubeGoogleService m_youTubeGoogleService;
        private readonly ISpotifyService m_spotifyService;
        private readonly IPartifyStorageService m_partifyStorageService;
        private readonly IRestClientWrapper m_restClientWrapper;

        public StorageController(IYouTubeGoogleService youTubeGoogleService, ISpotifyService spotifyService, IPartifyStorageService partifyStorageService, IRestClientWrapper restClientWrapper)
        {
            m_youTubeGoogleService = youTubeGoogleService;
            m_spotifyService = spotifyService;
            m_partifyStorageService = partifyStorageService;
            m_restClientWrapper = restClientWrapper;
        }


        [HttpGet("store")]
        public async Task<bool> Get()
        {
            string userId = this.Request.Query["userId"];
            string modeId = this.Request.Query["modeId"];
            string videoId = this.Request.Query["videoId"];
            string songId = this.Request.Query["songId"];
            if (!string.IsNullOrEmpty(songId) && !string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(modeId) && !string.IsNullOrEmpty(videoId))
            {
                var userIdDidParse = Guid.TryParse(userId, out var userIdGuid);
                var modeIdDidParse = Guid.TryParse(modeId, out var modeIdGuid);

                await m_partifyStorageService.AddSuggestion(videoId, songId, modeIdGuid, userIdGuid);

                return true;

            }

            return false;
        }
    }
}
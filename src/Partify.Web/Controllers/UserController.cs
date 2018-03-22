using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Partify.Server.Models;
using Partify.Server.Services;

namespace Partify.Web.Controllers
{
    [Route("/")]
    public class UserController : Controller
    {
        private readonly IYouTubeGoogleService m_youTubeGoogleService;
        private readonly ISpotifyService m_spotifyService;
        private readonly IPartifyStorageService m_partifyStorageService;
        private readonly IRestClientWrapper m_restClientWrapper;

        public UserController(IYouTubeGoogleService youTubeGoogleService, ISpotifyService spotifyService, IPartifyStorageService partifyStorageService, IRestClientWrapper restClientWrapper)
        {
            m_youTubeGoogleService = youTubeGoogleService;
            m_spotifyService = spotifyService;
            m_partifyStorageService = partifyStorageService;
            m_restClientWrapper = restClientWrapper;
        }


        [HttpGet("user")]
        public async Task<UserResult> Get()
        {
            string name = this.Request.Query["Name"];
            string contry = this.Request.Query["Contry"];
            string spotifyUserId = this.Request.Query["SpotifyUserId"];

            var user = new User { Country = contry, Name = name, SpotifyUserId = spotifyUserId };

            var userResult = await m_restClientWrapper.PostAsync<UserResult>(user, "User");
            return userResult;
        }

        [HttpGet("spotifyUser")]
        public async Task<SpotifyUser> Get1()
        {
            string token = this.Request.Query["token"];

            var user = await m_spotifyService.GetUser(token);

            return user;
        }

    }
}
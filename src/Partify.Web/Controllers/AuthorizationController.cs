using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Partify.Server.Models;
using Partify.Server.Services;

namespace Partify.Web.Controllers
{
    [Route("/")]
    public class AuthorizationController : Controller
    {
        private readonly IYouTubeGoogleService m_youTubeGoogleService;
        private readonly ISpotifyService m_spotifyService;
        private readonly IPartifyStorageService m_partifyStorageService;
        private readonly IRestClientWrapper m_restClientWrapper;
        private readonly IAuthorization m_authorization;

        public AuthorizationController(IYouTubeGoogleService youTubeGoogleService, ISpotifyService spotifyService, IPartifyStorageService partifyStorageService, IRestClientWrapper restClientWrapper, IAuthorization authorization)
        {
            m_youTubeGoogleService = youTubeGoogleService;
            m_spotifyService = spotifyService;
            m_partifyStorageService = partifyStorageService;
            m_restClientWrapper = restClientWrapper;
            m_authorization = authorization;
        }


        [HttpGet("token")]
        public async Task<TokenResponse> Get()
        {
            string code = this.Request.Query["code"];

            var tokenResponse = await m_authorization.GetToken(code);

            return tokenResponse;
        }

        [HttpGet("refreshToken")]
        public async Task<RefreshTokenResponse> Get2()
        {
            string refreshToken = this.Request.Query["refreshToken"];

            var tokenResponse = await m_authorization.RefreshToken(refreshToken);

            return tokenResponse;
        }
    }
}
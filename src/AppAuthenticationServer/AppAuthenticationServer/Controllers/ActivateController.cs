using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppAuthenticationServer.Model;
using AppAuthenticationServer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace AppAuthenticationServer.Controllers
{
    [Route("/[controller]")]
    public class ActivateController : Controller
    {
        readonly AuthService authService;
        readonly IMemoryCache memoryCache;
        readonly SpotifyAuthenticator spotifyAuthenticator;

        public ActivateController(AuthService authService, IMemoryCache memoryCache, SpotifyAuthenticator spotifyAuthenticator)
        {
            this.authService = authService;
            this.memoryCache = memoryCache;
            this.spotifyAuthenticator = spotifyAuthenticator;
        }

        [HttpGet("code")]
        public OneTimeCode Get() => authService.GetOneTimeCodeForDisplay();

        [HttpGet("code/{oneTimeCode}")]
        public SpotifySession Get(string oneTimeCode) => authService.GetSpotifySession(oneTimeCode);

        [HttpGet]
        public IActionResult Index()
        {
            const string Key = "activationwebsite";
            if (!memoryCache.TryGetValue(Key, out ViewResult view)) {
                view = View();
                memoryCache.Set(Key, view);
            }

            return view;
        }

        [HttpPost]
        public IActionResult Index(string activationKey)
        {
            if (authService.VerifySimpleCodeWasCorrect(activationKey)) {
                return new RedirectResult(spotifyAuthenticator.SpotifyAuthenticationURL, false, false);
            }

            // TODO: sm00there feilhåndtering
            return Content($"Wrong key {activationKey}");
        }

        [HttpGet("callback")]
        public async Task<IActionResult> SpotifyAuthCallback([FromQuery] string code)
        {
            await spotifyAuthenticator.GetSpotifyAccessToken(code);
            return Content($"SuccessfullyAuthenticated");
        }
    }
}

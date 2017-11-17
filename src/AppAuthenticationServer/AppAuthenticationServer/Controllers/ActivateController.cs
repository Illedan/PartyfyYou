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
        public IActionResult Index(string oneTimeCode)
        {
            if (authService.OneTimeCodeExists(oneTimeCode)) {
                return new RedirectResult(spotifyAuthenticator.GetSpotifyAuthenticationURL(oneTimeCode), false, false);
            }

            // TODO: sm00there feilhåndtering
            return Content($"Wrong key {oneTimeCode}");
        }

        [HttpGet("callback")]
        public async Task<IActionResult> SpotifyAuthCallback([FromQuery] string code, [FromQuery] string state)
        {
            if (authService.OneTimeCodeExists(state))
            {
                // TODO: sm00there feilhåndtering, noen har prøvde seg her..
                return Content($"Wrong key {state}");
            }

            // TODO: Hva skjer når bruker trykker cancel??? https://example.com/callback?error=access_denied&state=STATE
            var spotifySession = await spotifyAuthenticator.GetSpotifySession(code);
            authService.SetSpotifySession(state, spotifySession);
            return Content($"SuccessfullyAuthenticated");
        }
    }
}

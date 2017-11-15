using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AppAuthenticationServer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace AppAuthenticationServer.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        AuthService authService;

        public AuthController(AuthService authService) => this.authService = authService;

        // GET api/auth/f19a5452-9e3a-4768-84b7-a34aaa07c4bc 
        [HttpGet("{userId}")]
        public string Get(string userId)
        {
            // TODO: Check if user is authenticated (in cache)
            // TODO: Clear cache
            // TODO: Return token of some sort

            // Questions
            // TODO: Do all auth through this? 
            // TODO: Security? User certificate validation?
            // TODO: How to refresh on apple tv? 

            if (string.IsNullOrEmpty(userId))
            {
                return "";
            }

            return authService.GetTokenForUserId(userId);
        }

        // PUT api/auth/f19a5452-9e3a-4768-84b7-a34aaa07c4bc 
        [HttpPut("{userId}")]
        public void Put(string userId, [FromBody]string token)
        {
            // TODO: Kall denne når autentisert slik at tvapp kan hente token, men når er det?
            if (string.IsNullOrEmpty(userId))
            {
                return;
            }

            if (string.IsNullOrEmpty(token)) {
                return;
            }

            authService.SetTokenForUserId(userId, token);
        }
    }
}

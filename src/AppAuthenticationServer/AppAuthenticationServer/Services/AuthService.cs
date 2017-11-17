﻿using System;
using System.Net.Http;
using AppAuthenticationServer.Configuration;
using AppAuthenticationServer.Model;
using Microsoft.Extensions.Caching.Memory;

namespace AppAuthenticationServer.Services
{
    public class AuthService
    {
        readonly IMemoryCache memoryCache;

        readonly TimeSpan tokenTimeout;
        readonly Random random;

        public AuthService(IMemoryCache memoryCache) {
            this.memoryCache = memoryCache;

            tokenTimeout = TimeSpan.FromMinutes(5);
            random = new Random();
        } 

        public OneTimeCode GetOneTimeCodeForDisplay() {
            var oneTimeCode = "";
            do
            {
                oneTimeCode = random.Next(0, 99999).ToString("D5");;
            } while (memoryCache.TryGetValue(oneTimeCode, out object dummyObject));

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(tokenTimeout);
            memoryCache.Set(oneTimeCode, new object(), cacheEntryOptions);

            // TODO: Hent URL from service discovery
            return new OneTimeCode(oneTimeCode, "http://localhost:5000/activate/");
        }

        public bool VerifySimpleCodeWasCorrect(string simpleCode) {
            if (memoryCache.TryGetValue(simpleCode, out object dummyObject))
            {
                memoryCache.Remove(simpleCode);
                return true;
            }

            return false;
        }

        public SpotifySession GetSpotifySession(string oneTimeCode) {
            if (memoryCache.TryGetValue(oneTimeCode, out SpotifySession spotifySession)) {
                memoryCache.Remove(oneTimeCode);
                return spotifySession;
            }

            return new SpotifySession();
        }
            
        public void SetSpotifySession(string oneTimeCode, SpotifySession spotifySession) {
            memoryCache.Remove(oneTimeCode);
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(tokenTimeout);
            memoryCache.Set(oneTimeCode, spotifySession, cacheEntryOptions);
        }
    }
}

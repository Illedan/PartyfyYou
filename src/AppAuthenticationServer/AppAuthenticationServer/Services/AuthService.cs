using System;
using Microsoft.Extensions.Caching.Memory;

namespace AppAuthenticationServer.Services
{
    public class AuthService
    {
        readonly IMemoryCache memoryCache;
        readonly TimeSpan tokenTimeout;

        public AuthService(IMemoryCache memoryCache) {
            this.memoryCache = memoryCache;
            tokenTimeout = TimeSpan.FromMinutes(5);
        } 

        public string GetTokenForUserId(string userId) {
            if (memoryCache.TryGetValue(userId, out string token)) {
                memoryCache.Remove(userId);
                return token;
            }

            return "";
        }
            
        // TODO: Må ha noe token greier som bruker ser på skjermen som han kan skrive inn på websiden.
        // TODO: Ellers må bruker ha iOS appen slik at den kan la bruker trykke OK elno
        public void SetTokenForUserId(string userId, string token) {
            memoryCache.Remove(userId);
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(tokenTimeout);
            memoryCache.Set(userId, token, cacheEntryOptions);
        }
    }
}

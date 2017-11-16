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

        public string GetSimpleCodeForDisplay() {
            var simpleCode = "4444";
            memoryCache.Remove(simpleCode);
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(tokenTimeout);
            memoryCache.Set(simpleCode, "BQChhM6I4EK18P9ugbFYhNbl_JZn9znz5UZadQxHfzry2qEP2z9Nf8noXrrav9yYVSGK8ZoxobJYCyHkqMdDyc8s5RhU9Pry0hFjlpYYY1P1oPJMNvNA7Fzd8R97bGKY6X6Md_j-1N5VZOCfiOxjZQ", cacheEntryOptions);
            return simpleCode;
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

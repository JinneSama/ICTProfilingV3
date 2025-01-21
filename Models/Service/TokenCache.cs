using System;
using System.Runtime.Caching;

namespace Models.Service
{
    public class TokenCache
    {
        private static ObjectCache cache = MemoryCache.Default;
        private static string cacheName = "TokenCache";
        public static void StoreCache(string token)
        {
            CacheItemPolicy policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = DateTimeOffset.Now.AddHours(1);
            cache.Set(cacheName, token, policy);
        }

        public static string CheckCache()
        {
            var cachedToken = cache[cacheName] as string;
            if (cachedToken == null) return string.Empty;
            else return cachedToken;
        }
    }
}

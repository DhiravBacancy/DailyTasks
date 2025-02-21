using System;
using Microsoft.Extensions.Caching.Memory;

namespace ASP.NET_Core_Day4.Services
{
    public class CacheService
    {
        private readonly IMemoryCache _cache;
        private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(30); // Cache expiry time

        public CacheService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public void StoreGuid(string guid)
        {
            _cache.Set(guid, true, _cacheDuration);
        }

        public bool ValidateGuid(string guid)
        {
            return _cache.TryGetValue(guid, out _);
        }

        public void RemoveGuid(string guid)
        {
            _cache.Remove(guid);
        }
    }
}

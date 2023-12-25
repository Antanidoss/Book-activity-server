using BookActivity.Shared.Models;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace BookActivity.Domain.Cache
{
    public class UserCache
    {
        private readonly IMemoryCache _cache;

        MemoryCacheEntryOptions cacheEntryOptions = new();

        public UserCache(IMemoryCache cache)
        {
            _cache = cache;
            cacheEntryOptions.SetSlidingExpiration(TimeSpan.FromSeconds(50));
        }

        public void AddCurrentUser(CurrentUser currentUser)
        {
            var key = GetCurrentUserCacheKey(currentUser.Id);
            _cache.Set(key, currentUser, cacheEntryOptions);
        }

        public bool TryGetCurrentUser(Guid userId, out CurrentUser currentUser)
        {
            var key = GetCurrentUserCacheKey(userId);
            return _cache.TryGetValue(key, out currentUser);
        }

        public void RemoveCurrentUser(Guid userId)
        {
            var key = GetCurrentUserCacheKey(userId);
            _cache.Remove(key);
        }

        private string GetCurrentUserCacheKey(Guid userId)
        {
            return $"CurrentUser_{userId}";
        }
    }
}

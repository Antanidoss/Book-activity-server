using BookActivity.Domain.Queries.ActiveBookStatisticQueries;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace BookActivity.Domain.Cache
{
    internal sealed class ActiveBookStatisticCache
    {
        private readonly IMemoryCache _cache;

        public ActiveBookStatisticCache(IMemoryCache cache)
        {
            _cache = cache;
        }

        public void Add(Guid userId, ActiveBooksStatistic statistic)
        {
            var key = GetActiveBookStatisticCacheKey(userId);
            _cache.Set(key, statistic);
        }

        public ActiveBooksStatistic Get(Guid userId)
        {
            ActiveBooksStatistic statistic;
            var key = GetActiveBookStatisticCacheKey(userId);
            _cache.TryGetValue(key, out statistic);

            return statistic;
        }

        public void Remove(Guid userId)
        {
            var key = GetActiveBookStatisticCacheKey(userId);
            _cache.Remove(key);
        }

        private string GetActiveBookStatisticCacheKey(Guid userId)
        {
            return $"ActiveBookStatistic_{userId}";
        }
    }
}

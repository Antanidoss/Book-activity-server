using BookActivity.Domain.Queries.ActiveBookStatisticQueries.GetActiveBooksStatistic;
using BookActivity.Domain.Queries.ActiveBookStatisticQueries.GetActiveBooksStatisticByDay;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;

namespace BookActivity.Domain.Cache
{
    internal sealed class ActiveBookStatisticCache
    {
        private readonly IMemoryCache _cache;

        public ActiveBookStatisticCache(IMemoryCache cache)
        {
            _cache = cache;
        }

        public void AddActiveBookStatistic(Guid userId, ActiveBooksStatistic statistic)
        {
            var key = GetActiveBookStatisticCacheKey(userId);
            _cache.Set(key, statistic);
        }

        public void AddActiveBookStatisticByDay(Guid userId, DateTime date, IEnumerable<ActiveBookStatisticByDay> activeBookStatisticByDays)
        {
            var key = GetActiveBookStatisticByDayCacheKey(userId, date);
            _cache.Set(key, activeBookStatisticByDays);
        }

        public ActiveBooksStatistic GetActiveBookStatistic(Guid userId)
        {
            var key = GetActiveBookStatisticCacheKey(userId);
            _cache.TryGetValue(key, out ActiveBooksStatistic statistic);

            return statistic;
        }

        public IEnumerable<ActiveBookStatisticByDay> GetActiveBookStatisticByDay(Guid userId, DateTime date)
        {
            var key = GetActiveBookStatisticByDayCacheKey(userId, date);
            _cache.TryGetValue(key, out IEnumerable<ActiveBookStatisticByDay> statistic);

            return statistic;
        }

        public void RemoveActiveBookStatistic(Guid userId)
        {
            var key = GetActiveBookStatisticCacheKey(userId);
            _cache.Remove(key);
        }

        public void RemoveActiveBookStatisticByDay(Guid userId, DateTime date)
        {
            var key = GetActiveBookStatisticByDayCacheKey(userId, date);
            _cache.Remove(key);
        }

        private string GetActiveBookStatisticCacheKey(Guid userId)
        {
            return $"ActiveBookStatistic_{userId}";
        }

        private string GetActiveBookStatisticByDayCacheKey(Guid userId, DateTime date)
        {
            return $"ActiveBookStatisticByDay_{date}_{userId}";
        }
    }
}

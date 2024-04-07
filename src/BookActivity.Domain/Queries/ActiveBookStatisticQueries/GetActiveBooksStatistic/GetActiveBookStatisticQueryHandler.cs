using BookActivity.Domain.Cache;
using BookActivity.Domain.Constants;
using BookActivity.Domain.Events.ActiveBookEvent;
using BookActivity.Domain.Specifications.EventSpecs;
using MediatR;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookActivity.Domain.Queries.ActiveBookStatisticQueries.GetActiveBooksStatistic
{
    internal sealed class GetActiveBookStatisticQueryHandler : IRequestHandler<GetActiveBookStatisticQuery, ActiveBooksStatistic>
    {
        private readonly ActiveBookStatisticCache _cache;
        private readonly IMongoDatabase _mongoDbContext;

        public GetActiveBookStatisticQueryHandler(ActiveBookStatisticCache cache, IMongoDatabase mongoDbContext)
        {
            _cache = cache;
            _mongoDbContext = mongoDbContext;
        }

        public async Task<ActiveBooksStatistic> Handle(GetActiveBookStatisticQuery request, CancellationToken cancellationToken)
        {
            var activeBookStatistics = _cache.GetActiveBookStatistic(request.AppUserId);
            if (activeBookStatistics != null)
                return activeBookStatistics;

            var specification = new EventByUserIdSpec<UpdateActiveBookEvent>(request.AppUserId);

            var usersReadInfos = _mongoDbContext.GetCollection<UpdateActiveBookEvent>(EventMessageTypeConstants.UpdateActiveBook)
                .AsQueryable()
                .Where(specification)
                .ToArray();

            if (usersReadInfos == null || !usersReadInfos.Any())
                return new();

            activeBookStatistics = new()
            {
                AveragePagesReadPerDay = CalculateAveragePagesReadPerDay(usersReadInfos),
                AveragePagesReadPerWeek = CalculateAveragePagesReadPerWeek(usersReadInfos),
                AveragePagesReadPerMouth = CalculateAveragePagesReadPerMouth(usersReadInfos),
                NumberPagesReadPerYear = CalculateAveragePagesReadPerMouth(usersReadInfos),
                ReadingCalendar = GetСalendarStatistics(usersReadInfos)
            };

            _cache.AddActiveBookStatistic(request.AppUserId, activeBookStatistics);

            return activeBookStatistics;
        }

        private int CalculateAveragePagesReadPerDay(IEnumerable<UpdateActiveBookEvent> userReadInfos)
        {
            return (int)(userReadInfos
                .GroupBy(i => i.Timestamp.Date)
                .Select(g => g.Sum(i => i.CountPagesRead))
                .Average());
        }

        private int CalculateAveragePagesReadPerMouth(IEnumerable<UpdateActiveBookEvent> userReadInfos)
        {
            return (int)(userReadInfos
                .GroupBy(i => new { i.Timestamp.Year, i.Timestamp.Month })
                .Select(g => g.Sum(i => i.CountPagesRead))
                .Average());
        }

        private int CalculateAveragePagesReadPerWeek(IEnumerable<UpdateActiveBookEvent> userReadInfos)
        {
            return (int)(userReadInfos
                .GroupBy(i => CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(i.Timestamp, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday))
                .Select(g => g.Sum(i => i.CountPagesRead))
                .Average());
        }

        private IEnumerable<NumberOfPagesReadPerDay> GetСalendarStatistics(IEnumerable<UpdateActiveBookEvent> userReadInfos)
        {
            return userReadInfos
                .OrderBy(u => u.Timestamp.Date)
                .GroupBy(u => u.Timestamp.Date)
                .Select(g => new NumberOfPagesReadPerDay { CountPagesRead = g.Sum(u => u.CountPagesRead), Date = g.Key.ToString("dd-MM-yyyy") });
        }
    }
}

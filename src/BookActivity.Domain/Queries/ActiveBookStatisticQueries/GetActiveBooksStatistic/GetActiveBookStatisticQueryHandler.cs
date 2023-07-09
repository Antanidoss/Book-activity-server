using BookActivity.Domain.Cache;
using BookActivity.Domain.Constants;
using BookActivity.Domain.Events.ActiveBookEvent;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Specifications.StoredEventSpecs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace BookActivity.Domain.Queries.ActiveBookStatisticQueries.GetActiveBooksStatistic
{
    internal sealed class GetActiveBookStatisticQueryHandler : IRequestHandler<GetActiveBookStatisticQuery, ActiveBooksStatistic>
    {
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly ActiveBookStatisticCache _cache;

        public GetActiveBookStatisticQueryHandler(IEventStoreRepository eventStoreRepository, ActiveBookStatisticCache cache)
        {
            _eventStoreRepository = eventStoreRepository;
            _cache = cache;
        }

        public async Task<ActiveBooksStatistic> Handle(GetActiveBookStatisticQuery request, CancellationToken cancellationToken)
        {
            var activeBookStatistics = _cache.Get(request.AppUserId);
            if (activeBookStatistics != null)
                return activeBookStatistics;

            var specificatione = new StoredEventByMessageTypeSpec(EventMessageTypeConstants.UpdateActiveBook) & new StoredEventByUserIdSpec(request.AppUserId);
            var usersReadInfos = (await _eventStoreRepository.GetBySpecificationAsync(specificatione))
                .Select(e => JsonSerializer.Deserialize<UpdateActiveBookEvent>(e.Data))
                .Where(i => i.CountPagesRead > 0);

            if (usersReadInfos == null || !usersReadInfos.Any())
                return new();

            activeBookStatistics = new()
            {
                AveragePagesReadPerDay = CalculateAveragePagesReadPerDay(usersReadInfos),
                AveragePagesReadPerWeek = CalculateAveragePagesReadPerWeek(usersReadInfos),
                AveragePagesReadPerMouth = CalculateAveragePagesReadPerMouth(usersReadInfos),
                NumberPagesReadPerYear = GetAmountDaysOfReads(usersReadInfos),
                ReadingCalendar = GetСalendarStatistics(usersReadInfos)
            };

            _cache.Add(request.AppUserId, activeBookStatistics);

            return activeBookStatistics;
        }

        private int CalculateAveragePagesReadPerDay(IEnumerable<UpdateActiveBookEvent> userReadInfos)
        {
            return (int)userReadInfos
                .GroupBy(i => i.Timestamp.Date)
                .Select(g => g.Sum(i => i.CountPagesRead))
                .Average();
        }

        private int CalculateAveragePagesReadPerMouth(IEnumerable<UpdateActiveBookEvent> userReadInfos)
        {
            return (int)userReadInfos
                .GroupBy(i => new { i.Timestamp.Year, i.Timestamp.Month })
                .Select(g => g.Sum(i => i.CountPagesRead))
                .Average();
        }

        private int CalculateAveragePagesReadPerWeek(IEnumerable<UpdateActiveBookEvent> userReadInfos)
        {
            return (int)userReadInfos
                .GroupBy(i => CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(i.Timestamp, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday))
                .Select(g => g.Sum(i => i.CountPagesRead))
                .Average();
        }

        private int GetAmountDaysOfReads(IEnumerable<UpdateActiveBookEvent> userReadInfos)
        {
            return userReadInfos.Sum(i => i.CountPagesRead);
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

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

        public GetActiveBookStatisticQueryHandler(IEventStoreRepository eventStoreRepository)
        {
            _eventStoreRepository = eventStoreRepository;
        }

        public async Task<ActiveBooksStatistic> Handle(GetActiveBookStatisticQuery request, CancellationToken cancellationToken)
        {
            var specificatione = new StoredEventByMessageTypeSpec(EventMessageTypeConstants.UpdateActiveBook) & new StoredEventByUserIdSpec(request.AppUserId);

            var usersReadInfos = (await _eventStoreRepository.GetBySpecificationAsync(specificatione))
                .Select(e => JsonSerializer.Deserialize<UpdateActiveBookEvent>(e.Data))
                .Where(i => i.CountPagesRead > 0);

            if (usersReadInfos == null || !usersReadInfos.Any())
                return new();

            ActiveBooksStatistic activeBookStatistics = new()
            {
                AveragePagesReadPerDay = CalculateAveragePagesReadPerDay(usersReadInfos),
                AveragePagesReadPerMouth = CalculateAveragePagesReadPerMouth(usersReadInfos),
                AveragePagesReadPerWeek = CalculateAveragePagesReadPerWeek(usersReadInfos),
                AmountDaysOfReads = GetAmountDaysOfReads(usersReadInfos),
                ReadingCalendar = GetСalendarStatistics(usersReadInfos)
            };

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
            return userReadInfos
                .GroupBy(i => i.Timestamp.Date)
                .Count();
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

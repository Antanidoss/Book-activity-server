using BookActivity.Domain.Queries.ActiveBookStatisticQueries.GetActiveBooksStatistic;
using BookActivity.Domain.Queries.ActiveBookStatisticQueries.GetActiveBooksStatisticByDay;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookActivity.Application.Interfaces.Services
{
    public interface IActiveBookStatisticService
    {
        Task<ActiveBooksStatistic> GetActiveBookStatisticsAsync(Guid userId);
        Task<IEnumerable<ActiveBookStatisticByDay>> GetActiveBookStatisticByDayAsync(DateTime day, Guid userId);
    }
}

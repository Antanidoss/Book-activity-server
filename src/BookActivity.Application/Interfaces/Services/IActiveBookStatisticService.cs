using BookActivity.Domain.Queries.ActiveBookStatisticQueries;
using System;
using System.Threading.Tasks;

namespace BookActivity.Application.Interfaces.Services
{
    public interface IActiveBookStatisticService
    {
        Task<ActiveBooksStatistic> GetActiveBookStatistics(Guid userId);
    }
}

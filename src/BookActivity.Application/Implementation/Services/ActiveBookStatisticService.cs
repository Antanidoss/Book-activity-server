using BookActivity.Application.Interfaces.Services;
using BookActivity.Domain.Interfaces;
using BookActivity.Domain.Queries.ActiveBookStatisticQueries.GetActiveBooksStatistic;
using BookActivity.Domain.Queries.ActiveBookStatisticQueries.GetActiveBooksStatisticByDay;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookActivity.Application.Implementation.Services
{
    internal sealed class ActiveBookStatisticService : IActiveBookStatisticService
    {
        private readonly IMediatorHandler _mediator;

        public ActiveBookStatisticService(IMediatorHandler mediator)
        {
            _mediator = mediator;
        }

        public async Task<ActiveBooksStatistic> GetActiveBookStatisticsAsync(Guid userId)
        {
            GetActiveBookStatisticQuery query = new(userId);

            return await _mediator.SendQueryAsync(query);
        }

        public async Task<IEnumerable<ActiveBookStatisticByDay>> GetActiveBookStatisticByDayAsync(DateTime day, Guid userId)
        {
            GetActiveBooksStatisticByDayQuery query = new(day, userId);

            return await _mediator.SendQueryAsync(query);
        }
    }
}

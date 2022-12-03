using BookActivity.Application.Interfaces.Services;
using BookActivity.Domain.Interfaces;
using BookActivity.Domain.Queries.ActiveBookStatisticQueries;
using BookActivity.Domain.Queries.ActiveBookStatisticQueries.GetActiveBooksStatistic;
using System;
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

        public async Task<ActiveBooksStatistic> GetActiveBookStatistics(Guid userId)
        {
            var query = new GetActiveBookStatisticQuery(userId);

            return await _mediator.SendQuery(query);
        }
    }
}

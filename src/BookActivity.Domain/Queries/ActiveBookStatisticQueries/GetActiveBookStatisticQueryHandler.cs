using BookActivity.Domain.Interfaces.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookActivity.Domain.Queries.ActiveBookStatisticQueries
{
    internal sealed class GetActiveBookStatisticQueryHandler : IRequestHandler<GetActiveBookStatisticQuery, ActiveBookStatistics>
    {
        private readonly IEventStoreRepository _eventStoreRepository;

        public GetActiveBookStatisticQueryHandler(IEventStoreRepository eventStoreRepository)
        {
            _eventStoreRepository = eventStoreRepository;
        }

        public Task<ActiveBookStatistics> Handle(GetActiveBookStatisticQuery request, CancellationToken cancellationToken)
        {
            return null;
        }
    }
}

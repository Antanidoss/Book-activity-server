using BookActivity.Domain.Cache;
using BookActivity.Domain.Constants;
using BookActivity.Domain.Events.ActiveBookEvent;
using BookActivity.Domain.Filters;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Domain.Specifications.BookSpecs;
using BookActivity.Domain.Specifications.EventSpecs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookActivity.Domain.Queries.ActiveBookStatisticQueries.GetActiveBooksStatisticByDay
{
    internal sealed class GetActiveBooksStatisticByDayQueryHandler : IRequestHandler<GetActiveBooksStatisticByDayQuery, IEnumerable<ActiveBookStatisticByDay>>
    {
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly ActiveBookStatisticCache _cache;
        private readonly IBookRepository _bookRepository;

        public GetActiveBooksStatisticByDayQueryHandler(IEventStoreRepository eventStoreRepository, ActiveBookStatisticCache cache, IBookRepository bookRepository)
        {
            _eventStoreRepository = eventStoreRepository;
            _cache = cache;
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<ActiveBookStatisticByDay>> Handle(GetActiveBooksStatisticByDayQuery request, CancellationToken cancellationToken)
        {
            var activeBooksStatisticByDay = _cache.GetActiveBookStatisticByDay(request.AppUserId, request.Day);
            if (activeBooksStatisticByDay != null)
                return activeBooksStatisticByDay;

            var specification = new EventByUserIdSpec<UpdateActiveBookEvent>(request.AppUserId) & new EventByDateCreateSpec<UpdateActiveBookEvent>(request.Day);

            activeBooksStatisticByDay = (await _eventStoreRepository.GetBySpecificationAsync(EventMessageTypeConstants.UpdateActiveBook, specification))
                .Cast<UpdateActiveBookEvent>()
                .GroupBy(@event => @event.AggregateId)
                .Select(async groupping =>
                {
                    BookByActiveBookIdSpec bookByActiveBookIdSpec = new(groupping.Key);
                    DbSingleResultFilterModel<Book> filterModel = new(bookByActiveBookIdSpec, includes: b => b.ActiveBooks);
                    var book = await _bookRepository.GetByFilterAsync(bookByActiveBookIdSpec);

                    return new ActiveBookStatisticByDay
                    {
                        BookId = book?.Id ?? Guid.Empty,
                        BookTitle = book?.Title,
                        CountPagesRead = groupping.Sum(i => i.CountPagesRead)
                    };
                })
                .Select(task => task.Result)
                .ToArray();

            _cache.AddActiveBookStatisticByDay(request.AppUserId, request.Day, activeBooksStatisticByDay);

            return activeBooksStatisticByDay;
        }
    }
}

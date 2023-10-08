using BookActivity.Domain.Cache;
using BookActivity.Domain.Constants;
using BookActivity.Domain.Events.ActiveBookEvent;
using BookActivity.Domain.Filters;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Domain.Specifications.BookSpecs;
using BookActivity.Domain.Specifications.StoredEventSpecs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
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
            var specification = new StoredEventByMessageTypeSpec(EventMessageTypeConstants.UpdateActiveBook)
                & new StoredEventByUserIdSpec(request.AppUserId)
                & new StoredEventByDateCreate(request.Day);

            return (await _eventStoreRepository.GetBySpecificationAsync(specification))
                .Select(@event => JsonSerializer.Deserialize<UpdateActiveBookEvent>(@event.Data))
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
                        BookImageData = book != null ? Convert.ToBase64String(book?.ImageData) : null,
                        CountPagesRead = groupping.Sum(i => i.CountPagesRead)
                    };
                })
                .Select(task => task.Result);
        }
    }
}

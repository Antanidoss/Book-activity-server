using BookActivity.Domain.Cache;
using BookActivity.Domain.Constants;
using BookActivity.Domain.Events.ActiveBookEvent;
using BookActivity.Domain.Interfaces;
using BookActivity.Domain.Specifications.BookSpecs;
using BookActivity.Domain.Specifications.EventSpecs;
using MediatR;
using MongoDB.Driver;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookActivity.Domain.Queries.ActiveBookStatisticQueries.GetActiveBooksStatisticByDay
{
    internal sealed class GetActiveBooksStatisticByDayQueryHandler : IRequestHandler<GetActiveBooksStatisticByDayQuery, IEnumerable<ActiveBookStatisticByDay>>
    {
        private readonly ActiveBookStatisticCache _cache;
        private readonly IMongoDatabase _mongoDbContext;
        private readonly IDbContext _efDbContext;

        public GetActiveBooksStatisticByDayQueryHandler(ActiveBookStatisticCache cache, IMongoDatabase mongoDbContext, IDbContext efDbContext)
        {
            _cache = cache;
            _mongoDbContext = mongoDbContext;
            _efDbContext = efDbContext;
        }

        public async Task<IEnumerable<ActiveBookStatisticByDay>> Handle(GetActiveBooksStatisticByDayQuery request, CancellationToken cancellationToken)
        {
            var activeBooksStatisticByDay = _cache.GetActiveBookStatisticByDay(request.AppUserId, request.Day);
            if (activeBooksStatisticByDay != null)
                return activeBooksStatisticByDay;

            var specification = new EventByUserIdSpec<UpdateActiveBookEvent>(request.AppUserId) & new EventByDateCreateSpec<UpdateActiveBookEvent>(request.Day);

            activeBooksStatisticByDay = _mongoDbContext.GetCollection<UpdateActiveBookEvent>(EventMessageTypeConstants.UpdateActiveBook)
                .AsQueryable()
                .Where(specification)
                .GroupBy(@event => @event.AggregateId)
                .ToArray()
                .Select(async groupping =>
                {
                    BookByActiveBookIdSpec bookByActiveBookIdSpec = new(groupping.Key);
                    var book = await _efDbContext.Books.Where(bookByActiveBookIdSpec).Select(b => new { b.Id, b.Title }).FirstAsync();

                    return new ActiveBookStatisticByDay
                    {
                        BookId = book.Id,
                        BookTitle = book.Title,
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

using BookActivity.Domain.Filters.Handlers;
using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Shared.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookActivity.Domain.Queries.BookQueries
{
    internal sealed class GetBooksByFilterQueryHandler : IRequestHandler<GetBooksByFilterQuery, EntityListResult<Book>>
    {
        private readonly IBookRepository _bookRepository;

        public GetBooksByFilterQueryHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<EntityListResult<Book>> Handle(GetBooksByFilterQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            Func<IQueryable<Book>, IQueryable<Book>> filterWithPagination = (query) => query.ApplyBookFilter(request).ApplyPaginaton(request.Skip, request.Take);
            FilterModel<Book> filterModel = new(filterWithPagination, b => b.BookRating.BookOpinions);
            var books = await _bookRepository.GetByFilterAsync(filterModel).ConfigureAwait(false);

            var booksCount = await _bookRepository.GetCountByFilterAsync(query => query.ApplyBookFilter(request)).ConfigureAwait(false);

            return new EntityListResult<Book>(books, booksCount);
        }
    }
}

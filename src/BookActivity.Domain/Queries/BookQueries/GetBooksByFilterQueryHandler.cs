using BookActivity.Domain.Filters.Handlers;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Domain.Specifications.BookSpecs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookActivity.Domain.Queries.BookQueries
{
    internal sealed class GetBooksByFilterQueryHandler : IRequestHandler<GetBooksByFilterQuery, IEnumerable<Book>>
    {
        private readonly IBookRepository _bookRepository;

        public GetBooksByFilterQueryHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<Book>> Handle(GetBooksByFilterQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            Func<IQueryable<Book>, IQueryable<Book>> filter = (query) =>
            {
                if (!string.IsNullOrEmpty(request.BookTitle))
                {
                    BookByTitleContainsSpec bookByTitleSpec = new(request.BookTitle);

                    query = query.Where(bookByTitleSpec.ToExpression());
                }

                if (request.AverageRatingFrom != 0 || request.AverageRatingTo != 5)
                {
                    BookByRatingRange bookByRatingRangeSpec = new(request.AverageRatingFrom, request.AverageRatingTo);

                    query = query.Where(bookByRatingRangeSpec.ToExpression());
                }

                return query.ApplyPaginaton(request.Skip, request.Take);
            };

            return await _bookRepository.GetByFilterAsync(filter, b => b.BookRating, b => b.BookRating.BookOpinions).ConfigureAwait(false);
        }
    }
}

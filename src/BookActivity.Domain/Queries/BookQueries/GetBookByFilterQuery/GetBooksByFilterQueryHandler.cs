using BookActivity.Domain.Filters;
using BookActivity.Domain.Filters.Handlers;
using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Interfaces;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookActivity.Domain.Queries.BookQueries.GetBookByFilterQuery
{
    internal sealed class GetBooksByFilterQueryHandler : IRequestHandler<GetBooksByFilterQuery, EntityListResult<SelectedBook>>
    {
        private readonly IBookRepository _bookRepository;

        private readonly IFilterHandler<Book, GetBooksByFilterQuery> _filterHandler;

        private readonly IFilterSelectHandler<Book, IEnumerable<SelectedBook>, GetBooksByFilterQuery> _filterSelectHandler;

        public GetBooksByFilterQueryHandler(IBookRepository bookRepository, IFilterHandler<Book, GetBooksByFilterQuery> filterHandler, IFilterSelectHandler<Book, IEnumerable<SelectedBook>, GetBooksByFilterQuery> filterSelectHandler)
        {
            _bookRepository = bookRepository;
            _filterHandler = filterHandler;
            _filterSelectHandler = filterSelectHandler;
        }

        public async Task<EntityListResult<SelectedBook>> Handle(GetBooksByFilterQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var filterWithPagination = GetFilterWithPagination(request);
            DbMultipleResultFilterModel<Book, IEnumerable<SelectedBook>> filterModel = new(filterWithPagination, b => b.BookRating.BookOpinions);
            var books = await _bookRepository.GetByFilterAsync(filterModel).ConfigureAwait(false);

            DbMultipleResultFilterModel<Book> filterModelForCount = new(GetFilter(request), new PaginationModel(request.Skip, request.Take));
            var booksCount = await _bookRepository.GetCountByFilterAsync(filterModelForCount).ConfigureAwait(false);

            return new EntityListResult<SelectedBook>(books, booksCount);
        }

        private Func<IQueryable<Book>, Task<IEnumerable<SelectedBook>>> GetFilterWithPagination(GetBooksByFilterQuery filterModel)
        {
            return async query =>
            {
                query = _filterHandler.ApplyFilter(query, filterModel);

                query = query.ApplyPaginaton(new PaginationModel(filterModel.Skip, filterModel.Take));

                return await _filterSelectHandler.Select(query, filterModel);
            };
        }

        private Func<IQueryable<Book>, IQueryable<Book>> GetFilter(GetBooksByFilterQuery filterModel)
        {
            return query => _filterHandler.ApplyFilter(query, filterModel);
        }
    }
}

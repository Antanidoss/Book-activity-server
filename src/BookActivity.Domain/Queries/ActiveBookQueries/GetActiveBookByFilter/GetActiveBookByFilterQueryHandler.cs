using MediatR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookActivity.Shared.Models;
using BookActivity.Domain.Filters.Models;
using System.Threading;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Interfaces;
using BookActivity.Domain.Models;
using BookActivity.Domain.Queries.BookQueries.GetBookByFilterQuery;
using System.Linq;
using BookActivity.Domain.Filters.Handlers;

namespace BookActivity.Domain.Queries.ActiveBookQueries.GetActiveBookByFilter
{
    internal sealed class GetActiveBookByFilterQueryHandler : IRequestHandler<GetActiveBookByFilterQuery, EntityListResult<SelectedActiveBook>>
    {
        private readonly IActiveBookRepository _activeBookRepository;

        private readonly IFilterHandler<ActiveBook, GetActiveBookByFilterQuery> _filterHandler;

        private readonly IFilterSelectHandler<ActiveBook, IEnumerable<SelectedActiveBook>, GetActiveBookByFilterQuery> _filterSelectHandler;

        public GetActiveBookByFilterQueryHandler(IActiveBookRepository activeBookRepository, IFilterHandler<ActiveBook, GetActiveBookByFilterQuery> filterHandler, IFilterSelectHandler<ActiveBook, IEnumerable<SelectedActiveBook>, GetActiveBookByFilterQuery> filterSelectHandler)
        {
            _activeBookRepository = activeBookRepository;
            _filterHandler = filterHandler;
            _filterSelectHandler = filterSelectHandler;
        }

        public async Task<EntityListResult<SelectedActiveBook>> Handle(GetActiveBookByFilterQuery request, CancellationToken cancellationToken)
        {

            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var filterWithPagination = GetFilterWithPagination(request);
            var activeBooks = await _activeBookRepository.GetByFilterAsync(filterWithPagination, b => b.Book).ConfigureAwait(false);

            var booksCount = await _activeBookRepository.GetCountByFilterAsync(GetFilter(request)).ConfigureAwait(false);

            return new EntityListResult<SelectedActiveBook>(activeBooks, booksCount);
        }

        private Func<IQueryable<ActiveBook>, Task<IEnumerable<SelectedActiveBook>>> GetFilterWithPagination(GetActiveBookByFilterQuery filterModel)
        {
            return async (query) =>
            {
                query = _filterHandler.ApplyFilter(query, filterModel);

                query = query.ApplyPaginaton(filterModel.Skip, filterModel.Take);

                return await _filterSelectHandler.Select(query, filterModel);
            };
        }

        private Func<IQueryable<ActiveBook>, IQueryable<ActiveBook>> GetFilter(GetActiveBookByFilterQuery filterModel)
        {
            return (query) => _filterHandler.ApplyFilter(query, filterModel);
        }
    }
}

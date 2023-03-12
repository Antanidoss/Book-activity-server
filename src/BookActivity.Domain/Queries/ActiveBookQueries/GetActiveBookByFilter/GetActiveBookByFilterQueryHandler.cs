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
using System.Linq;
using BookActivity.Domain.Filters.Handlers;
using BookActivity.Domain.Filters;

namespace BookActivity.Domain.Queries.ActiveBookQueries.GetActiveBookByFilter
{
    internal sealed class GetActiveBookByFilterQueryHandler : IRequestHandler<GetActiveBookByFilterQuery, EntityListResult<SelectedActiveBook>>
    {
        private readonly IActiveBookRepository _activeBookRepository;

        private readonly IFilterHandler<ActiveBook, GetActiveBookByFilterQuery> _filterHandler;

        private readonly IFilterSelectHandler<ActiveBook, IEnumerable<SelectedActiveBook>, GetActiveBookByFilterQuery> _filterSelectHandler;

        public GetActiveBookByFilterQueryHandler(
            IActiveBookRepository activeBookRepository,
            IFilterHandler<ActiveBook, GetActiveBookByFilterQuery> filterHandler,
            IFilterSelectHandler<ActiveBook, IEnumerable<SelectedActiveBook>, GetActiveBookByFilterQuery> filterSelectHandler)
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
            DbMultipleResultFilterModel<ActiveBook, IEnumerable<SelectedActiveBook>> filterModel = new(filterWithPagination, b => b.Book.BookRating.BookOpinions);

            var activeBooks = await _activeBookRepository.GetByFilterAsync(filterModel)
                .ConfigureAwait(false);

            DbMultipleResultFilterModel<ActiveBook> filterModelForCount = new(GetFilter(request));

            var booksCount = await _activeBookRepository.GetCountByFilterAsync(filterModelForCount)
                .ConfigureAwait(false);

            return new EntityListResult<SelectedActiveBook>(activeBooks, booksCount);
        }

        private Func<IQueryable<ActiveBook>, Task<IEnumerable<SelectedActiveBook>>> GetFilterWithPagination(GetActiveBookByFilterQuery filterModel)
        {
            return async query =>
            {
                query = _filterHandler.ApplyFilter(query, filterModel);

                query = query.ApplyPaginaton(new PaginationModel(filterModel.Skip, filterModel.Take), withDefaultOrder: false);

                return await _filterSelectHandler.Select(query, filterModel);
            };
        }

        private Func<IQueryable<ActiveBook>, IQueryable<ActiveBook>> GetFilter(GetActiveBookByFilterQuery filterModel)
        {
            return query => _filterHandler.ApplyFilter(query, filterModel);
        }
    }
}

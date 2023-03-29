using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookActivity.Domain.Filters.Models;
using System.Threading;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Filters.Handlers;
using BookActivity.Domain.Interfaces;
using BookActivity.Domain.Models;
using BookActivity.Shared.Models;
using BookActivity.Domain.Filters;

namespace BookActivity.Domain.Queries.AppUserQueries.GetUsersByFilter
{
    internal sealed class GetUsersByFilterQueryHandler : IRequestHandler<GetUsersByFilterQuery, EntityListResult<SelectedAppUser>>
    {
        private readonly IAppUserRepository _appUserRepository;

        private readonly IFilterHandler<AppUser, GetUsersByFilterQuery> _filterHandler;

        private readonly IFilterSelectHandler<AppUser, IEnumerable<SelectedAppUser>, GetUsersByFilterQuery> _filterSelectHandler;

        public GetUsersByFilterQueryHandler(IAppUserRepository appUserRepository, IFilterHandler<AppUser, GetUsersByFilterQuery> filterHandler, IFilterSelectHandler<AppUser, IEnumerable<SelectedAppUser>, GetUsersByFilterQuery> selectFilterHandler)
        {
            _appUserRepository = appUserRepository;
            _filterHandler = filterHandler;
            _filterSelectHandler = selectFilterHandler;
        }

        public async Task<EntityListResult<SelectedAppUser>> Handle(GetUsersByFilterQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var filterWithPagination = GetFilterWithPagination(request);
            DbMultipleResultFilterModel<AppUser, IEnumerable<SelectedAppUser>> filterModel = new(filterWithPagination);
            var users = await _appUserRepository.GetByFilterAsync(filterModel).ConfigureAwait(false);

            var usersCount = await _appUserRepository.GetCountByFilterAsync(GetFilter(request)).ConfigureAwait(false);

            return new EntityListResult<SelectedAppUser>(users, usersCount);
        }

        private Func<IQueryable<AppUser>, Task<IEnumerable<SelectedAppUser>>> GetFilterWithPagination(GetUsersByFilterQuery filterModel)
        {
            return async query =>
            {
                query = _filterHandler.ApplyFilter(query, filterModel);

                query = query.ApplyPaginaton(new PaginationModel(filterModel.Skip, filterModel.Take));

                return await _filterSelectHandler.Select(query, filterModel);
            };
        }

        private Func<IQueryable<AppUser>, IQueryable<AppUser>> GetFilter(GetUsersByFilterQuery filterModel)
        {
            return query => _filterHandler.ApplyFilter(query, filterModel);
        }
    }
}

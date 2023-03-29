using BookActivity.Domain.Interfaces;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookActivity.Domain.Queries.AppUserQueries.GetUserProfileInfo
{
    internal sealed class GetUserProfileInfoQueryHandler : IRequestHandler<GetUserProfileInfoQuery, AppUserProfileInfo>
    {
        private readonly IAppUserRepository _appUserRepository;

        private readonly IFilterSelectHandler<AppUser, AppUserProfileInfo, GetUserProfileInfoQuery> _filterSelectHandler;

        public GetUserProfileInfoQueryHandler(IAppUserRepository appUserRepository, IFilterSelectHandler<AppUser, AppUserProfileInfo, GetUserProfileInfoQuery> filterSelectHandler)
        {
            _appUserRepository = appUserRepository;
            _filterSelectHandler = filterSelectHandler;
        }

        public async Task<AppUserProfileInfo> Handle(GetUserProfileInfoQuery request, CancellationToken cancellationToken)
        {
            var selectHandler = GetSelectHandler(request);

            return await _appUserRepository.GetByFilterAsync<AppUserProfileInfo>(selectHandler);
        }

        private Func<IQueryable<AppUser>, Task<AppUserProfileInfo>> GetSelectHandler(GetUserProfileInfoQuery filterModel)
        {
            return async query => await _filterSelectHandler.Select(query, filterModel);
        }
    }
}

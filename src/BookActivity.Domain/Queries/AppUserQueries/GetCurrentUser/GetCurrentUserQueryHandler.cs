using BookActivity.Domain.Cache;
using BookActivity.Domain.Interfaces;
using BookActivity.Domain.Specifications.AppUserSpecs;
using BookActivity.Domain.Validations;
using BookActivity.Shared.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookActivity.Domain.Queries.AppUserQueries.GetCurrentUser
{
    internal sealed class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, CurrentUser>
    {
        private readonly UserCache _userCache;
        private readonly IDbContext _efContext;

        public GetCurrentUserQueryHandler(UserCache userCache, IDbContext efContext)
        {
            _userCache = userCache;
            _efContext = efContext;
        }

        public async Task<CurrentUser> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
        {
            if (_userCache.TryGetCurrentUser(request.UserId, out CurrentUser currentUser))
                return currentUser;

            AppUserByIdSpec specification = new(request.UserId);
            currentUser = await _efContext.Users
                .Where(specification)
                .Select(u => new CurrentUser
                {
                    Id = u.Id,
                    AvatarImage = u.AvatarImage,
                    UserName = u.UserName,
                })
                .FirstAsync();

            _userCache.AddCurrentUser(currentUser);

            return currentUser;
        }
    }
}

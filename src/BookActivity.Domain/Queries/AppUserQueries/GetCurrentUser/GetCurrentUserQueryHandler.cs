using BookActivity.Domain.Cache;
using BookActivity.Domain.Interfaces;
using BookActivity.Domain.Models;
using BookActivity.Domain.Specifications.AppUserSpecs;
using BookActivity.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<AppUser> _userManager;

        public GetCurrentUserQueryHandler(UserCache userCache, IDbContext efContext, UserManager<AppUser> userManager)
        {
            _userCache = userCache;
            _efContext = efContext;
            _userManager = userManager;
        }

        public async Task<CurrentUser> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
        {
            //if (_userCache.TryGetCurrentUser(request.UserId, out CurrentUser currentUser))
            //    return currentUser;

            AppUserByIdSpec specification = new(request.UserId);
            var currentUser = await _efContext.Users
                .Where(specification)
                .Select(u => new CurrentUser
                {
                    Id = u.Id,
                    AvatarImage = u.AvatarImage,
                    UserName = u.UserName,
                    Roles = _userManager.GetRolesAsync(u).GetAwaiter().GetResult().ToArray()
                })
                .FirstAsync();

            _userCache.AddCurrentUser(currentUser);

            return currentUser;
        }
    }
}

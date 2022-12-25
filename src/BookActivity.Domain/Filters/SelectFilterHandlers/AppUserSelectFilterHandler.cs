using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Interfaces;
using BookActivity.Domain.Models;
using BookActivity.Domain.Queries.AppUserQueries.GetUsersByFilter;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookActivity.Domain.Filters.SelectFilterHandlers
{
    internal sealed class AppUserSelectFilterHandler : IFilterSelectHandler<AppUser, IEnumerable<SelectedAppUser>, GetUsersByFilterQuery>
    {
        public async Task<IEnumerable<SelectedAppUser>> Select(IQueryable<AppUser> query, GetUsersByFilterQuery filterModel)
        {
            return query.Select(u => new SelectedAppUser
            {
                Id = u.Id,
                UserName = u.UserName,
                AvatarImage = u.AvatarImage,
            }).ToList();
        }
    }
}

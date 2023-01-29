using BookActivity.Domain.Interfaces;
using BookActivity.Domain.Models;
using BookActivity.Domain.Queries.AppUserQueries.GetUsersByFilter;
using BookActivity.Domain.Specifications.AppUserSpecs;
using System.Linq;

namespace BookActivity.Domain.Filters.FilterHandlers
{
    internal sealed class AppUserFilterHandler : IFilterHandler<AppUser, GetUsersByFilterQuery>
    {
        public IQueryable<AppUser> ApplyFilter(IQueryable<AppUser> query, GetUsersByFilterQuery filterModel)
        {
            if (!string.IsNullOrEmpty(filterModel.Name))
            {
                AppUserByNameSpec userByNameSpec = new(filterModel.Name);
                query = query.Where(userByNameSpec);
            }

            return query;
        }
    }
}

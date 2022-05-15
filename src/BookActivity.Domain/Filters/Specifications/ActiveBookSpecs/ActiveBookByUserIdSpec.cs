using BookActivity.Domain.Interfaces.Filters;
using BookActivity.Domain.Models;
using System;
using System.Linq;

namespace BookActivity.Domain.Filters.Specifications.ActiveBookSpecs
{
    public sealed class ActiveBookByUserIdSpec : IQueryableFilterSpec<ActiveBook, Guid>
    {
        public IQueryable<ActiveBook> ApplyFilter(IQueryable<ActiveBook> entities, Guid userId)
        {
            return entities.Where(a => a.UserId == userId);
        }
    }
}
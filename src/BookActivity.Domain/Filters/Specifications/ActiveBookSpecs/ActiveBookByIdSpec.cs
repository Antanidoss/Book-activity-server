using BookActivity.Domain.Interfaces.Filters;
using BookActivity.Domain.Models;
using System;
using System.Linq;

namespace BookActivity.Domain.Filters.Specifications.ActiveBookSpecs
{
    public sealed class ActiveBookByIdSpec : IQueryableFilterSpec<ActiveBook, Guid>
    {
        public IQueryable<ActiveBook> ApplyFilter(IQueryable<ActiveBook> activeBooks, Guid activeBookId)
        {
            return activeBooks.Where(a => a.Id == activeBookId);
        }
    }
}

using BookActivity.Domain.Interfaces.Filters;
using System;
using System.Linq;

namespace BookActivity.Domain.Filters.Specifications.ActiveBook
{
    public sealed class ActiveBookByUserIdSpec : IQueryableSpecification<Domain.Models.ActiveBook>
    {
        private readonly Guid _userId;

        public ActiveBookByUserIdSpec(Guid userId)
        {
            _userId = userId;
        }

        public IQueryable<Domain.Models.ActiveBook> Apply(IQueryable<Domain.Models.ActiveBook> query) => query.Where(a => a.UserId == _userId);
    }
}
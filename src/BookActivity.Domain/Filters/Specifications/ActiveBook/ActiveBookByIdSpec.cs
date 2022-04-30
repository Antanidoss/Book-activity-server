using BookActivity.Domain.Interfaces.Filters;
using System;
using System.Linq;

namespace BookActivity.Domain.Filters.Specifications.ActiveBook
{
    public sealed class ActiveBookByIdSpec : IQueryableSpecification<Domain.Models.ActiveBook>
    {
        private readonly Guid _activeBookId;

        public ActiveBookByIdSpec(Guid activeBookId)
        {
            _activeBookId = activeBookId;
        }

        public IQueryable<Domain.Models.ActiveBook> Apply(IQueryable<Domain.Models.ActiveBook> query) => query.Where(a => a.Id == _activeBookId);
    }
}

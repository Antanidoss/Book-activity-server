using Antanidoss.Specification.Abstract;
using BookActivity.Domain.Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BookActivity.Domain.Specifications.ActiveBookSpecs
{
    public sealed class ActiveBookByUserIdSpec : Specification<ActiveBook>
    {
        private readonly Guid _userId;

        public ActiveBookByUserIdSpec(Guid userId)
        {
            _userId = userId;
        }

        public IQueryable<ActiveBook> ApplyFilter(IQueryable<ActiveBook> activeBooks)
        {
            return activeBooks.Where(ToExpression());
        }

        public override Expression<Func<ActiveBook, bool>> ToExpression()
        {
            return a => a.UserId == _userId;
        }
    }
}
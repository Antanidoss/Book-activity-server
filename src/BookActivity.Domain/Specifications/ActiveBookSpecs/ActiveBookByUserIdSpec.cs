using Antanidoss.Specification.Interfaces;
using BookActivity.Domain.Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BookActivity.Domain.Specifications.ActiveBookSpecs
{
    public sealed class ActiveBookByUserIdSpec : ISpecification<ActiveBook>
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

        public Expression<Func<ActiveBook, bool>> ToExpression()
        {
            return a => a.UserId == _userId;
        }
    }
}
using Antanidoss.Specification.Abstract;
using BookActivity.Domain.Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BookActivity.Domain.Specifications.AuthorSpecs
{
    public sealed class AuthorByIdSpec : Specification<Author>
    {
        private readonly Guid[] _authorIds;

        public AuthorByIdSpec(params Guid[] authorIds)
        {
            _authorIds = authorIds;
        }

        public override Expression<Func<Author, bool>> ToExpression()
        {
            return a => _authorIds.Contains(a.Id);
        }
    }
}
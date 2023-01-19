using Antanidoss.Specification.Abstract;
using BookActivity.Domain.Models;
using System;
using System.Linq.Expressions;

namespace BookActivity.Domain.Specifications.AuthorSpecs
{
    public sealed class AuthorByNameSpec : Specification<Author>
    {
        private readonly string _name;

        public AuthorByNameSpec(string name)
        {
            _name = name;
        }

        public override Expression<Func<Author, bool>> ToExpression()
        {
            return a => a.FirstName.Contains(_name) || a.Surname.Contains(_name) || a.Patronymic.Contains(_name);
        }
    }
}

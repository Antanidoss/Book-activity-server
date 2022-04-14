using BookActivity.Domain.Models;
using System;
using System.Linq.Expressions;

namespace BookActivity.Application.Models.Filters
{
    public class ActiveBookFilterModel : BaseFilterModel
    {
        public Expression<Func<ActiveBook, bool>> Condition { get; set; }

        public ActiveBookFilterModel(int skip, int take, Expression<Func<ActiveBook, bool>> condition) : base(skip, take)
        {
            Condition = condition;
        }
    }
}
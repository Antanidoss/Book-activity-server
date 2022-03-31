using BookActivity.Domain.Models;
using System;
using System.Linq.Expressions;

namespace BookActivity.Application.Models.Filters
{
    public class ActiveBookFilterModel : BaseFilterModel
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public Expression<Func<ActiveBook, bool>> Condition { get; set; }
    }
}
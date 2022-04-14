using BookActivity.Domain.Models;
using System;
using System.Linq.Expressions;

namespace BookActivity.Application.Models.Filters
{
    public class BookFilterModel : BaseFilterModel
    {
        public Expression<Func<Book, bool>> Condtion { get; set; }

        public BookFilterModel(int skip, int take, Expression<Func<Book, bool>> condtion) : base(skip, take)
        {
            Condtion = condtion;
        }
    }
}
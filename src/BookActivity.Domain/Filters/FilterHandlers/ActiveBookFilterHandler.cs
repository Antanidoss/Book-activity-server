using BookActivity.Domain.Interfaces;
using BookActivity.Domain.Models;
using BookActivity.Domain.Queries.ActiveBookQueries.GetActiveBookByFilter;
using BookActivity.Domain.Specifications.BookSpecs;
using System;
using System.Data.Entity;
using System.Linq;

namespace BookActivity.Domain.Filters.FilterHandlers
{
    public class ActiveBookFilterHandler : IFilterHandler<ActiveBook, GetActiveBookByFilterQuery>
    {
        public IQueryable<ActiveBook> ApplyFilter(IQueryable<ActiveBook> query, GetActiveBookByFilterQuery filterModel)
        {
            if (filterModel.UserId == Guid.Empty)
                throw new ArgumentException($"{nameof(filterModel.UserId)} cannot be empty");

            query = query.Where(a => a.UserId == filterModel.UserId);

            if (!filterModel.WithFullRead)
                query = query.Where(a => a.NumberPagesRead != a.TotalNumberPages);

            if (!string.IsNullOrEmpty(filterModel.BookTitle))
            {
                query = query.Include(a => a.Book);
                BookByTitleContainsSpec bookByTitleSpec = new(filterModel.BookTitle);

                query = query.Where(a => bookByTitleSpec.IsSatisfy(a.Book));
            }

            switch (filterModel.SortBy)
            {
                case SortByType.CreateDateUp:
                    query = query.OrderBy(a => a.TimeOfCreation);
                    break;
                case SortByType.CreateDateDown:
                    query = query.OrderByDescending(a => a.TimeOfCreation);
                    break;
                case SortByType.UpdateDate:
                    query = query.OrderBy(a => a.TimeOfUpdate);
                    break;
            }

            return query;
        }
    }
}

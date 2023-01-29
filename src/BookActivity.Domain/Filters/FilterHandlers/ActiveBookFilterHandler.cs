using BookActivity.Domain.Interfaces;
using BookActivity.Domain.Models;
using BookActivity.Domain.Queries.ActiveBookQueries.GetActiveBookByFilter;
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
                var bookTitle = filterModel.BookTitle.Replace(" ", string.Empty).ToLower();

                query = query.Include(b => b.Book).Where(a => a.Book.Title.Replace(" ", string.Empty).ToLower() == bookTitle);
            }

            switch (filterModel.SortBy)
            {
                case SortByType.CreateDate:
                    query = query.OrderBy(a => a.TimeOfCreation);
                    break;
                case SortByType.CreateDateDescending:
                    query = query.OrderByDescending(a => a.TimeOfCreation);
                    break;
                case SortByType.UpdateDateDescending:
                    query = query.OrderByDescending(a => a.TimeOfUpdate);
                    break;
            }

            return query;
        }
    }
}

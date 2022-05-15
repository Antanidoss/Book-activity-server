using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Interfaces.Filters;
using BookActivity.Domain.Models;
using System;
using System.Linq;

namespace BookActivity.Application.Implementation.FilterHandlers
{
    public sealed class ActiveBookFilterHandler : IFilterHandler<ActiveBook, ActiveBookFilterModel>
    {
        public IQueryable<ActiveBook> Handle(ActiveBookFilterModel activeBookFilterModel, IQueryable<ActiveBook> query)
        {
            if (query == null) return query;

            if (activeBookFilterModel.ActiveBookId.Value == Guid.Empty)
                query = activeBookFilterModel.ActiveBookId.FilterSpec.ApplyFilter(query, activeBookFilterModel.ActiveBookId.Value);

            if (activeBookFilterModel.UserId.Value == Guid.Empty)
                query = activeBookFilterModel.UserId.FilterSpec.ApplyFilter(query, activeBookFilterModel.UserId.Value);

            return query.Skip(activeBookFilterModel.Skip).Take(activeBookFilterModel.Take);
        }
    }
}

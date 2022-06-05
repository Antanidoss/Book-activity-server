using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Interfaces.Filters;
using BookActivity.Domain.Models;
using System.Linq;

namespace BookActivity.Application.Implementation.FilterHandlers
{
    internal sealed class ActiveBookFilterHandler : IFilterHandler<ActiveBook, ActiveBookFilterModel>
    {
        public IQueryable<ActiveBook> Handle(ActiveBookFilterModel activeBookFilterModel, IQueryable<ActiveBook> query)
        {
            if (query == null) return query;

            if (activeBookFilterModel.ActiveBookIds != null)
                query = activeBookFilterModel.ActiveBookIds.FilterSpec.ApplyFilter(query, activeBookFilterModel.ActiveBookIds.Value);

            if (activeBookFilterModel.UserId != null)
                query = activeBookFilterModel.UserId.FilterSpec.ApplyFilter(query, activeBookFilterModel.UserId.Value);

            return query.Skip(activeBookFilterModel.Skip).Take(activeBookFilterModel.Take);
        }
    }
}

using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Filters.Specifications.ActiveBook;
using BookActivity.Domain.Models;
using System;
using System.Linq;

namespace BookActivity.Domain.Filters.FilterFacades
{
    public sealed class ActiveBookFilter : BaseFilter<ActiveBook>
    {
        private readonly ActiveBookFilterModel _activeBookFilterModel;

        public ActiveBookFilter(ActiveBookFilterModel activeBookFilterModel) : base(activeBookFilterModel)
        {
            _activeBookFilterModel = activeBookFilterModel;
        }

        public override IQueryable<ActiveBook> ApplyFilter(IQueryable<ActiveBook> query)
        {
            if (query == null) return query;

            if (_activeBookFilterModel.ActiveBookId == Guid.Empty)
            {
                ActiveBookByIdSpec activeBookByIdSpec = new(_activeBookFilterModel.ActiveBookId);
                query = activeBookByIdSpec.Apply(query);
            }
            else if (_activeBookFilterModel.UserId == Guid.Empty)
            {
                ActiveBookByUserIdSpec activeBookByUserId = new(_activeBookFilterModel.UserId);
                query = activeBookByUserId.Apply(query);
            }

            return base.ApplyFilter(query);
        }
    }
}

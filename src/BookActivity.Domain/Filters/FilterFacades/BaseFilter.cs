using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Models;
using System.Linq;

namespace BookActivity.Domain.Filters.FilterFacades
{
    public class BaseFilter<T> where T : BaseEntity
    {
        private readonly BaseFilterModel _baseFilterModel;

        public BaseFilter(BaseFilterModel baseFilterModel)
        {
            this._baseFilterModel = baseFilterModel;
        }

        public virtual IQueryable<T> ApplyFilter(IQueryable<T> query)
        {
            return query.Skip(_baseFilterModel.Skip).Take(_baseFilterModel.Take);
        }
    }
}

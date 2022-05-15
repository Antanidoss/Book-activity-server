using BookActivity.Domain.Interfaces.Filters;
using BookActivity.Domain.Models;
using System.Linq;

namespace BookActivity.Domain.Filters.Models
{
    public class FilterModelProp<TEntityType, TPropType>
        where TEntityType : BaseEntity
    {
        public TPropType Value { get; private set; }
        public IQueryableFilterSpec<TEntityType, TPropType> FilterSpec { get; private set; }

        public FilterModelProp(TPropType propValue, IQueryableFilterSpec<TEntityType, TPropType> filterSpec)
        {
            Value = propValue;
            FilterSpec = filterSpec;
        }
    }
}

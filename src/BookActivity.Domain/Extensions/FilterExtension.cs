using BookActivity.Domain.Filters;
using BookActivity.Domain.Interfaces.Filters;
using BookActivity.Domain.Models;

namespace BookActivity.Domain.Extensions
{
    public static class FilterExtension
    {
        public static IQueryableFilterSpec<TEntityType> And<TEntityType>(this IQueryableFilterSpec<TEntityType> firstFilter, IQueryableFilterSpec<TEntityType> secondFilter)
            where TEntityType : BaseEntity
        {
            return new AndIQueryableFilterSpec<TEntityType>(firstFilter, secondFilter);
        }

        public static IQueryableFilterSpec<TEntityType> Or<TEntityType>(this IQueryableFilterSpec<TEntityType> firstFilter, IQueryableFilterSpec<TEntityType> secondFilter)
            where TEntityType : BaseEntity
        {
            return new OrIQueryableFilterSpec<TEntityType>(firstFilter, secondFilter);
        }
    }
}

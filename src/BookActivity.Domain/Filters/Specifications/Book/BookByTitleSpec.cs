using BookActivity.Domain.Interfaces.Filters;
using System.Linq;

namespace BookActivity.Models.Filters.Specifications.Book
{
    public class BookByTitleSpec : IQueryableSpecification<Domain.Models.Book>
    {
        private readonly string _title;
        public BookByTitleSpec(string title)
        {
            _title = title;
        }
        public IQueryable<Domain.Models.Book> Apply(IQueryable<Domain.Models.Book> query) => query.Where(b => b.Title == _title);
    }
}

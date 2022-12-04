using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Interfaces;
using BookActivity.Domain.Models;
using BookActivity.Domain.Queries.ActiveBookQueries.GetActiveBookByFilter;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace BookActivity.Domain.Filters.SelectFilterHandlers
{
    internal sealed class ActiveBookSelectFilterHandler : IFilterSelectHandler<ActiveBook, IEnumerable<SelectedActiveBook>, GetActiveBookByFilterQuery>
    {
        public async Task<IEnumerable<SelectedActiveBook>> Select(IQueryable<ActiveBook> query, GetActiveBookByFilterQuery filterModel)
        {
            return query.Include(a => a.BookNotes).Select(a => new SelectedActiveBook
            {
                Id = a.Id,
                BookId = a.BookId,
                BookTitle = a.Book.Title,
                ImageData = a.Book.ImageData,
                NumberPagesRead = a.NumberPagesRead,
                TotalNumberPages = a.TotalNumberPages,
                BookNotes = a.BookNotes.Select(n => new SelectedBookNote
                {
                    Id = n.Id,
                    Note = n.Note,
                    NoteColor = (int)n.NoteColor
                }),
            }).ToList();
        }
    }
}

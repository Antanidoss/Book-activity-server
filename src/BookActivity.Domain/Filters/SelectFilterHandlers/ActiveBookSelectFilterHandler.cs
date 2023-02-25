using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Interfaces;
using BookActivity.Domain.Models;
using BookActivity.Domain.Queries.ActiveBookQueries.GetActiveBookByFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BookActivity.Domain.Filters.Models.SelectedActiveBook;

namespace BookActivity.Domain.Filters.SelectFilterHandlers
{
    internal sealed class ActiveBookSelectFilterHandler : IFilterSelectHandler<ActiveBook, IEnumerable<SelectedActiveBook>, GetActiveBookByFilterQuery>
    {
        public async Task<IEnumerable<SelectedActiveBook>> Select(IQueryable<ActiveBook> query, GetActiveBookByFilterQuery filterModel)
        {
            return query
                .Select(a => new SelectedActiveBook
                {
                    Id = a.Id,
                    BookId = a.BookId,
                    BookTitle = a.Book.Title,
                    ImageData = a.Book.ImageData,
                    NumberPagesRead = a.NumberPagesRead,
                    TotalNumberPages = a.TotalNumberPages,
                    BookRatingId = a.Book.BookRating.Id,
                    BookOpinion = GetBookOpinion(a, filterModel.UserId),
                    BookNotes = a.BookNotes.Select(n => new SelectedBookNote
                    {
                        Id = n.Id,
                        Note = n.Note,
                        NoteColor = (int)n.NoteColor
                    }),
                }).ToList();
        }

        private static SelectedBookOpinion GetBookOpinion(ActiveBook activeBook, Guid userId)
        {
            var bookOpinion = activeBook.Book.BookRating.BookOpinions.FirstOrDefault(o => o.UserId == userId);

            return bookOpinion == null
                ? null
                : new SelectedBookOpinion { Id = bookOpinion.Id, Description = bookOpinion.Description, Grade = bookOpinion.Grade };
        }
    }
}

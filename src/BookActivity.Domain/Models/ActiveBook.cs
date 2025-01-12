using BookActivity.Domain.Core;
using System;
using System.Collections.Generic;

namespace BookActivity.Domain.Models
{
    public sealed class ActiveBook : BaseEntity
    {
        public int TotalNumberPages { get; set; }
        public int NumberPagesRead { get; set; }
        public Book Book { get; private set; }
        public Guid BookId { get; private set; }
        public AppUser User { get; private set; }
        public Guid UserId { get; private set; }
        public ICollection<BookNote> BookNotes { get; set; }

        public ActiveBook(int totalNumberPages, int numberPagesRead, Guid bookId, Guid userId)
        {
            TotalNumberPages = totalNumberPages;
            NumberPagesRead = numberPagesRead;
            BookId = bookId;
            UserId = userId;
        }

        //private, parameterless constructor used by EF Core
        private ActiveBook() { }

        public ActiveBook(Guid activeBookId)
        {
            Id = activeBookId;
        }
    }
}
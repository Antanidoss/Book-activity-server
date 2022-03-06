using System;
using System.Collections.Generic;

namespace BookActivity.Domain.Models
{
    public class ActiveBook : BaseEntity
    {
        /// <summary>
        /// Total number of pages
        /// </summary>
        public int TotalNumberPages { get; set; }

        /// <summary>
        /// Number of pages read
        /// </summary>
        public int NumberPagesRead { get; set; }

        /// <summary>
        /// Relation of active book with the book
        /// </summary>
        public Book Book { get; private set; }
        public Guid BookId { get; private set; }

        /// <summary>
        /// Relation of active book with the user
        /// </summary>
        public AppUser User { get; private set; }
        public Guid UserId { get; private set; }

        /// <summary>
        /// Relation of active book with the book notes
        /// </summary>
        public ICollection<BookNote> BookNotes { get; set; }

        protected ActiveBook() : base() { }
        public ActiveBook(Guid activeBookId, int totalNumberPages, int numberPagesRead, Guid bookId, Guid userId, bool isPublic) : base(isPublic)
        {
            Id = activeBookId;
            TotalNumberPages = totalNumberPages;
            NumberPagesRead = numberPagesRead;
            BookId = bookId;
            UserId = userId;
        }
    }
}
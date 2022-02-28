using BookActivity.Domain.Constants;
using BookActivity.Domain.Helpers;
using System.ComponentModel.DataAnnotations;

namespace BookActivity.Domain.Models
{
    public class ActiveBook : BaseEntity
    {
        /// <summary>
        /// Total number of pages
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = ValidationErrorMessage.RangeMessage)]
        public int TotalNumberPages
        {
            get
            {
                return totalNumberPages;
            }
            set
            {
                ValidationModelHelper.ValidateProperty(this, value, nameof(TotalNumberPages));
                totalNumberPages = value;
            }
        }
        private int totalNumberPages { get; set; }

        /// <summary>
        /// Number of pages read
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = ValidationErrorMessage.RangeMessage)]
        public int NumberPagesRead
        {
            get
            {
                return numberPagesRead;
            }
            set
            {
                ValidationModelHelper.ValidateProperty(this, value, nameof(NumberPagesRead));
                numberPagesRead = value;
            }
        }
        private int numberPagesRead { get; set; }

        /// <summary>
        /// Relation of active book with the book
        /// </summary>
        public Book Book { get; private set; }
        public int BookId { get; private set; }

        /// <summary>
        /// Relation of active book with the user
        /// </summary>
        public AppUser User { get; private set; }
        public int UserId { get; private set; }

        protected ActiveBook() : base() { }
        public ActiveBook(int totalNumberPages, int numberPagesRead, int bookId, int userId) : base()
        {
            TotalNumberPages = totalNumberPages;
            NumberPagesRead = numberPagesRead;
            BookId = bookId;
            UserId = userId;
        }
    }
}
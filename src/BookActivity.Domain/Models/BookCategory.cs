using BookActivity.Domain.Constants;
using BookActivity.Domain.Helpers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookActivity.Domain.Models
{
    public class BookCategory : BaseEntity
    {
        [Required(ErrorMessage = ValidationErrorMessage.RequiredMessage)]
        [StringLength(30, MinimumLength = 3, ErrorMessage = ValidationErrorMessage.StrLengthMessage)]
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                ValidationModelHelper.ValidateProperty(this, value, nameof(Title));
                title = value;
            }
        }
        private string title { get; set; }
        public ICollection<Book> Books { get; set; }

        protected BookCategory() : base() { }
        public BookCategory(string title) : base()
        {
            Title = title;
            Books = new List<Book>();
        }
        public BookCategory(string title, params Book[] books) : base()
        {
            Title = title;
            Books = books;
        }
    }
}

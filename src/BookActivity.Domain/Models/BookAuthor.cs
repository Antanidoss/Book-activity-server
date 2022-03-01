using BookActivity.Domain.Constants;
using BookActivity.Domain.Helpers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookActivity.Domain.Models
{
    public class BookAuthor : BaseEntity
    {
        /// <summary>
        /// Firstname author
        /// </summary>
        [Required(ErrorMessage = ValidationErrorMessage.RequiredMessage)]
        [StringLength(50, MinimumLength = 2, ErrorMessage = ValidationErrorMessage.StrLengthMessage)]
        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                ValidationModelHelper.ValidateProperty(this, value, nameof(FirstName));
                firstName = value;
            }
        }
        private string firstName { get; set; }

        /// <summary>
        /// Surname author
        /// </summary>
        [Required(ErrorMessage = ValidationErrorMessage.RequiredMessage)]
        [StringLength(50, MinimumLength = 2, ErrorMessage = ValidationErrorMessage.StrLengthMessage)]
        public string Surname
        {
            get
            {
                return surname;
            }
            set
            {
                ValidationModelHelper.ValidateProperty(this, value, nameof(Surname));
                surname = value;
            }
        }
        private string surname { get; set; }

        /// <summary>
        /// Patronymic author
        /// </summary>
        [Required(ErrorMessage = ValidationErrorMessage.RequiredMessage)]
        [StringLength(50, MinimumLength = 2, ErrorMessage = ValidationErrorMessage.StrLengthMessage)]
        public string Patronymic
        {
            get
            {
                return patronymic;
            }
            set
            {
                ValidationModelHelper.ValidateProperty(this, value, nameof(Patronymic));
                patronymic = value;
            }
        }
        private string patronymic { get; set; }

        /// <summary>
        /// Relation of author with the book authors
        /// </summary>
        public ICollection<Book> Books { get; set; }

        protected BookAuthor() : base() { }
        public BookAuthor(string firstName, string surname, string patronymic, bool isPublic, params Book[] books) : base(isPublic)
        {
            FirstName = firstName;
            Surname = surname;
            Patronymic = patronymic;
            Books = books;
        }
    }
}
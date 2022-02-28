using BookActivity.Domain.Constants;
using BookActivity.Domain.Helpers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookActivity.Domain.Models
{
    public class Book : BaseEntity
    {
        /// <summary>
        /// Book title
        /// </summary>
        [Required(ErrorMessage = ValidationErrorMessage.RangeMessage)]
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

        /// <summary>
        /// Book description
        /// </summary>
        [Required(ErrorMessage = ValidationErrorMessage.RangeMessage)]
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                ValidationModelHelper.ValidateProperty(this, value, nameof(Description));
                description = value;
            }
        }
        private string description { get; set; }

        /// <summary>
        /// Relation of book with the book authors
        /// </summary>
        public IEnumerable<BookAuthor> Authors { get; private set; }

        /// <summary>
        /// Relation of book with the book opinion
        /// </summary>
        public IList<BookOpinion> BookOpinions { get; set; }

        protected Book() : base() { }
        public Book(string title, string description, params BookAuthor[] authors) : base()
        {
            Title = title;
            Description = description;
            Authors = authors;
            BookOpinions = new List<BookOpinion>();
        }
    }
}
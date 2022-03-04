using System.Collections.Generic;

namespace BookActivity.Domain.Models
{
    public class Book : BaseEntity
    {
        /// <summary>
        /// Book title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Book description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Relation of book with the book authors
        /// </summary>
        public IEnumerable<BookAuthor> Authors { get; private set; }

        /// <summary>
        /// Relation of book with the book opinion
        /// </summary>
        public IList<BookOpinion> BookOpinions { get; set; }

        protected Book() : base() { }
        public Book(string title, string description, bool isPublic, params BookAuthor[] authors) : base(isPublic)
        {
            Title = title;
            Description = description;
            Authors = authors;
            BookOpinions = new List<BookOpinion>();
        }
    }
}
using BookActivity.Domain.Core;
using System;

namespace BookActivity.Domain.Models
{
    public sealed class BookOpinion : BaseEntity
    {
        /// <summary>
        /// User rating of the book
        /// </summary>
        public float Grade { get; set; }

        /// <summary>
        /// Opinion Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Relation of opinion with the user
        /// </summary>
        public AppUser User { get; private set; }
        public Guid UserId { get; private set; }

        /// <summary>
        /// Relation of book opinion with the book
        /// </summary>
        public Book Book { get; set; }
        public Guid BookId { get; set; }

        public const int GradeMin = 0;
        public const int GradeMax = 5;

        public BookOpinion() : base() { }
        public BookOpinion(float grade, string description, Guid userId, Guid bookId)
        {
            Grade = grade;
            Description = description;
            UserId = userId;
            BookId = bookId;
        }
    }
}
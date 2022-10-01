using System;
using System.Collections.Generic;

namespace BookActivity.Domain.Models
{
    public sealed class BookOpinion : BaseEntity
    {
        /// <summary>
        /// User rating of the book
        /// </summary>
        public int Grade { get; set; }

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
        /// Relation of book opinion with the book rating
        /// </summary>
        public BookRating BookRating { get; set; }
        public Guid BookRatingId { get; set; }

        private BookOpinion() : base() { }
        public BookOpinion(int grade, string description, Guid userId, bool isPublic) : base(isPublic)
        {
            Grade = grade;
            Description = description;
            UserId = userId;
        }
    }
}
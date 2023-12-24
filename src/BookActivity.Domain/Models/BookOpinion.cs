using BookActivity.Domain.Core;
using System;
using System.Collections.Generic;

namespace BookActivity.Domain.Models
{
    public sealed class BookOpinion : BaseEntity
    {
        public float Grade { get; set; }
        public string Description { get; set; }
        public AppUser User { get; private set; }
        public Guid UserId { get; private set; }
        public Book Book { get; set; }
        public Guid BookId { get; set; }
        public ICollection<BookOpinionLike> Likes { get; private set; }
        public ICollection<BookOpinionDislike> Dislikes { get; private set; }

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
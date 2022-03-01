using BookActivity.Domain.Constants;
using BookActivity.Domain.Helpers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookActivity.Domain.Models
{
    public class BookOpinion : BaseEntity
    {
        /// <summary>
        /// User rating of the book
        /// </summary>
        [Required(ErrorMessage = ValidationErrorMessage.RequiredMessage)]
        [Range(0, 5, ErrorMessage = ValidationErrorMessage.RangeMessage)]
        public int Grade
        {
            get
            {
                return grade;
            }
            set
            {
                ValidationModelHelper.ValidateProperty(this, value, nameof(Grade));
                grade = value;
            }
        }
        private int grade { get; set; }

        /// <summary>
        /// Opinion Description
        /// </summary>
        [Required(ErrorMessage = ValidationErrorMessage.RequiredMessage)]
        [StringLength(1500, MinimumLength = 50, ErrorMessage = ValidationErrorMessage.StrLengthMessage)]
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
        /// Relation of opinion with the user
        /// </summary>
        public AppUser User { get; private set; }
        public int UserId { get; private set; }

        /// <summary>
        /// Relation of book opinion with the book
        /// </summary>
        public Book Book { get; private set; }
        public int BookId { get; private set; }

        /// <summary>
        /// Relation of book opinion with the response opinions
        /// </summary>
        public ICollection<ResponseOpinion> ResponseOpinions { get; set; }

        protected BookOpinion() : base() { }
        public BookOpinion(int grade, string description, int userId, int bookId, bool isPublic) : base(isPublic)
        {
            Grade = grade;
            Description = description;
            UserId = userId;
            BookId = bookId;
            ResponseOpinions = new List<ResponseOpinion>();
        }
    }
}